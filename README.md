**快彩分析平台---后台接口**


---

## 架构

Lottery后台系统采用基于Enode的 `DDD+CQRS + EDA + Event Sourcing + In Memory` 分布式框架,Enode是一个基于消息的分布式框架,有良好的可伸缩和扩展性。

```
-- Hosts

   -- Lottery.BrokerService
   -- Lottery.CommandService
   -- Lottery.EventService
   -- Lottery.NameServerService
   -- Lottery.RunApp
   -- Lottery.Web
   -- Lottery.WebApi
``` 
   

---

## 彩票数据引擎


---

## 接口

