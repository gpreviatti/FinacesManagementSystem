using System;

namespace Domain.Helpers;

public static class WalletTypeHelper
{
    public static Guid CheckingAccount { get; } = Guid.Parse("AD4AC47F-0888-4D60-81F9-964153B13E37");

    public static Guid Credit { get; } = Guid.Parse("AD4AC47F-0888-4D60-81F9-964153B13E38");

    public static Guid Saving { get; } = Guid.Parse("AD4AC47F-0888-4D60-81F9-964153B13E39");

    public static Guid Investiments { get; } = Guid.Parse("AD4AC47F-0888-4D60-81F9-964153B13E40");

    public static Guid Stocks { get; } = Guid.Parse("AD4AC47F-0888-4D60-81F9-964153B13E41");
}
