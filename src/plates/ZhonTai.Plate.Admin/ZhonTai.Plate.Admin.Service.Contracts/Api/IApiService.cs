using ZhonTai.Common.Domain.Dto;
using System.Threading.Tasks;
using ZhonTai.Plate.Admin.Service.Api.Dto;
using ZhonTai.Plate.Admin.Domain.Api.Dto;
using ZhonTai.Tools.DynamicApi;
using ZhonTai.Tools.DynamicApi.Attributes;

namespace ZhonTai.Plate.Admin.Service.Api
{
    /// <summary>
    /// �ӿڷ���
    /// </summary>
    [DynamicApi(Area = "admin")]
    public interface IApiService: IDynamicApi
    {
        /// <summary>
        /// ���һ����¼
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultOutput> GetAsync(long id);

        /// <summary>
        /// ����б�
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<IResultOutput> GetListAsync(string key);

        /// <summary>
        /// ��÷�ҳ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> GetPageAsync(PageInput<ApiGetPageDto> input);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> AddAsync(ApiAddInput input);

        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> UpdateAsync(ApiUpdateInput input);

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultOutput> DeleteAsync(long id);

        /// <summary>
        /// ��ɾ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultOutput> SoftDeleteAsync(long id);

        /// <summary>
        /// ������ɾ��
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IResultOutput> BatchSoftDeleteAsync(long[] ids);

        /// <summary>
        /// ͬ��
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> SyncAsync(ApiSyncInput input);
    }
}