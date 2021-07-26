using DMS.Common.BaseResult;
using DMS.Common.Extensions;
using DMS.Common.Serialization;
using DMS.Redis;
using DMS.Redis.Tickets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Freesql.API.Filters
{
    /// <summary>
    /// 属性检查登录
    /// </summary>
    public class CheckLoginAttribute : ActionFilterAttribute, IActionFilter
    {

        /// <summary>
        /// 用户票据
        /// </summary>
        public UserTicket UserTicket
        {
            get;
            private set;
        }
        /// <summary>
        /// 
        /// </summary>
        public RedisManager redisManager { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RedisManager RedisManager
        {
            get
            {
                if (redisManager == null)
                {
                    redisManager = new RedisManager(0);
                }
                return redisManager;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Microsoft.Extensions.Primitives.StringValues token = context.HttpContext.Request.Headers["AccessToken"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                //存在AccessToken值，进行验证
                var userTicket = RedisManager.StringGet<UserTicket>(token);
                if (userTicket != null && userTicket.ID.ToLong() > 0)
                {
                    UserTicket = userTicket;
                    return;
                }
            }

            //直接输出结果，不经过Controller
            ResponseResult result = new ResponseResult()
            {
                errno = 30,
                errmsg = "请重新登录",
            };
            context.Result = new ContentResult() { Content = result.SerializeObject(), StatusCode = 200 };
        }
    }
}
