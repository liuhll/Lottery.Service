﻿namespace Lottery.AppService.Operations
{
    public interface IMemberAppService
    {
        string ConcludeUserMemRank(string userId, string clientTypeId);
    }
}