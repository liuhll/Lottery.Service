using ECommon.Components;
using ECommon.Extensions;
using Lottery.Core.Caching;
using Lottery.Dtos.Menus;
using Lottery.Dtos.Power;
using Lottery.Infrastructure;
using Lottery.Infrastructure.Enums;
using Lottery.Infrastructure.Exceptions;
using Lottery.Infrastructure.Extensions;
using Lottery.QueryServices.Powers;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.AppService.Power
{
    [Component]
    public class PowerManager : IPowerManager
    {
        private readonly IPowerQueryService _powerQueryService;
        private readonly ICacheManager _cacheManager;
        private readonly IUserPowerQueryService _userPowerQueryService;

        public PowerManager(IPowerQueryService powerQueryService,
            IUserPowerQueryService userPowerQueryService,
            ICacheManager cacheManager)
        {
            _powerQueryService = powerQueryService;
            _userPowerQueryService = userPowerQueryService;
            _cacheManager = cacheManager;
        }

        public PowerDto GetPermission(string powerCode)
        {
            return _powerQueryService.GetPermissionByCode(powerCode);
        }

        public PowerDto GetPermission(string urlPath, string method)
        {
            return _powerQueryService.GetPermissionByApi(urlPath, method);
        }

        public ICollection<RouteDto> GetUserRoutes(string userId, SystemType systemType)
        {
            ICollection<RouteDto> routeDtos = null;
            switch (systemType)
            {
                case SystemType.App:
                    routeDtos = GetUserAppRoutes(userId);
                    break;

                case SystemType.BackOffice:
                    routeDtos = GetUseBoRoutes(userId);
                    break;

                case SystemType.OfficialWebsite:
                    throw new LotteryException("门户网站无法调用该接口");
                default:
                    break;
            }
            return routeDtos;
        }

        protected virtual ICollection<RouteDto> GetUseBoRoutes(string userId)
        {
            var cacheKey = string.Format(RedisKeyConstants.LOTTERY_BO_USER_POWER_KEY, userId);
            return _cacheManager.Get<ICollection<RouteDto>>(cacheKey, () =>
            {
                var boPowers = _powerQueryService.GetUserBoPowers(userId);
                var boRouters = BuildRouteByPowers(boPowers);
                return boRouters;
            });
        }

        private ICollection<RouteDto> BuildRouteByPowers(ICollection<PowerDto> appPowers)
        {
            var routes = new List<RouteDto>();
            var hiddenRouters = BuildHiddenRouters(appPowers.Where(p => p.PowerType == 0));
            routes.AddRange(hiddenRouters);
            var tabbarRouters = BuildNavBarSelfRouters(appPowers.Where(p => p.PowerType != 0).ToList(), true);
            routes.AddRange(tabbarRouters);
            return routes;
        }

        private IEnumerable<RouteDto> BuildNavBarSelfRouters(ICollection<PowerDto> powers, bool isRoot, string parentId = "")
        {
            IList<RouteDto> routes = new List<RouteDto>();
            if (isRoot)
            {
                var rootPowers = powers.Where(p => p.ParentId.IsNullOrEmpty());
                foreach (var rootPower in rootPowers)
                {
                    var rootRoute = AutoMapper.Mapper.Map<RouteDto>(rootPower);
                    if (!rootPower.Meta.IsNullOrEmpty())
                    {
                        rootRoute.Meta = rootPower.Meta.ToObject<MetaDto>();
                    }
                    rootRoute.Children = BuildNavBarSelfRouters(powers, false, rootPower.Id).ToList();
                    if (!rootRoute.Path.IsNullOrEmpty())
                    {
                        routes.Add(rootRoute);
                    }
                }
            }
            else
            {
                var childrenPowers = powers.Where(p => p.ParentId == parentId);
                if (childrenPowers.Safe().Any())
                {
                    foreach (var power in childrenPowers)
                    {
                        var route = AutoMapper.Mapper.Map<RouteDto>(power);
                        if (!power.Meta.IsNullOrEmpty())
                        {
                            route.Meta = power.Meta.ToObject<MetaDto>();
                        }
                        route.Children = BuildNavBarSelfRouters(powers, false, power.Id).ToList();
                        if (!route.Path.IsNullOrEmpty())
                        {
                            routes.Add(route);
                        }
                    }
                }
            }

            return routes;
        }

        private IEnumerable<RouteDto> BuildHiddenRouters(IEnumerable<PowerDto> powers)
        {
            var hiddenRts = new List<RouteDto>();
            foreach (var power in powers)
            {
                var route = AutoMapper.Mapper.Map<RouteDto>(power);
                if (!power.Meta.IsNullOrEmpty())
                {
                    route.Meta = power.Meta.ToObject<MetaDto>();
                }
                route.Hidden = true;
                hiddenRts.Add(route);
            }
            return hiddenRts;
        }

        protected virtual ICollection<RouteDto> GetUserAppRoutes(string userId)
        {
            return _cacheManager.Get<ICollection<RouteDto>>(RedisKeyConstants.LOTTERY_APP_POWER_ALL_KEY, () =>
            {
                var appPowers = _powerQueryService.GetAppPowers();
                var appRouters = BuildRouteByPowers(appPowers);
                return appRouters;
            });
        }
    }
}