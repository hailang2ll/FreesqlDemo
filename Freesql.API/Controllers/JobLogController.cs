using DMS.Auth;
using DMS.Auth.Tickets;
using DMS.Redis;
using DMSN.Common.BaseResult;
using Freesql.Contracts;
using Freesql.Contracts.Param;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freesql.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class JobLogController : BaseController
    {
        private readonly ISysJobLogService jobLogService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobLogService"></param>
        public JobLogController(ISysJobLogService jobLogService)
        {
            this.jobLogService = jobLogService;
        }
        /// <summary>
        /// 新增工作日志
        /// [HttpPost("Add"), CheckLogin]
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<ResponseResult> AddAsync(AddJobLogParam param)
        {
            //#region 验证登录
            //var (loginFlag, result) = ChenkLogin(UserTicket);
            //if (!loginFlag)
            //{
            //    return await Task.FromResult(result);
            //}
            //#endregion
            //var appid = Request.Headers["appid"];
            //var accessToken = Request.Headers["AccessToken"];
            //long currentUser = UserTicket.ID;



            #region 缓存测试
            RedisManager redisManager = new RedisManager(0);
            UserTicket userTicket = new UserTicket
            {
                ID = 1234567890,
                ExpDate = DateTime.Now,
                Code = 0,
                Msg = "成功0",
                Name = "肖浪",
            };
            var b = redisManager.StringSet("dylan", userTicket);
            var v = redisManager.StringGet<UserTicket>("dylan");
            #endregion

            return await jobLogService.AddAsync(param);
        }

        /// <summary>
        /// 事物处理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("AddTran")]
        public async Task<ResponseResult> AddTranAsync(AddJobLogParam param)
        {
            return await jobLogService.AddTranAsync(param);
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="jobLogID"></param>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<ResponseResult> DeleteAsync(long jobLogID)
        {
            return await jobLogService.DeleteAsync(jobLogID);
        }
        /// <summary>
        /// 修改日志
        /// </summary>
        /// <param name="jobLogID"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<ResponseResult> UpdateAsync(long jobLogID)
        {
            return await jobLogService.UpdateAsync(jobLogID);
        }

        /// <summary>
        /// 获取工作日志记录
        /// </summary>
        /// <param name="jobLogID"></param>
        /// <returns></returns>
        [HttpGet("GetJobLog")]
        public async Task<ResponseResult<JobLogResult>> GetJobLogAsync(long jobLogID)
        {

            return await jobLogService.GetJobLogAsync(jobLogID);
        }

        /// <summary>
        /// 获取日志集合
        /// </summary>
        /// <param name="jobLogType"></param>
        /// <returns></returns>
        [HttpGet("GetJobLogList")]
        public async Task<ResponseResult<List<JobLogResult>>> GetJobLogListAsync(long jobLogType)
        {
            return await jobLogService.GetJobLogListAsync(jobLogType);
        }
        /// <summary>
        /// 搜索日志
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        [HttpGet("SearchJobLog")]
        public async Task<ResponsePageResult<JobLogResult>> SearchJobLogAsync([FromQuery] SearchJobLogParam param)
        {
            return await jobLogService.SearchJobLogAsync(param);
        }
    }
}
