using Helpers.Attributes;

namespace Helpers.Enums
{
    public enum EWalletType
    {
        [Guid("AD4AC47F-0888-4D60-81F9-964153B13E37")]
        CheckingAccount,

        [Guid("AD4AC47F-0888-4D60-81F9-964153B13E38")]
        Credit,

        [Guid("AD4AC47F-0888-4D60-81F9-964153B13E39")]
        Saving,

        [Guid("AD4AC47F-0888-4D60-81F9-964153B13E40")]
        Investiments,

        [Guid("AD4AC47F-0888-4D60-81F9-964153B13E41")]
        Stocks
    }
}
