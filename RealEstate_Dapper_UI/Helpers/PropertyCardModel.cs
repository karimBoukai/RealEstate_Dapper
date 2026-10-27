namespace RealEstate_Dapper_UI.Helpers
{
    /// <summary>
    /// View-only shape for the shared property card partial, so the different listing DTOs
    /// (ResultProductDtos, GetProductsForAdsPageDtos, ResultProductWithSearchListDtos) render identically.
    /// </summary>
    public record PropertyCardModel(
        int Id,
        string? Title,
        decimal Price,
        string? Type,
        string? Category,
        string? Address,
        string? District,
        string? City,
        string? CoverImage,
        string Url,
        bool Eager = false);
}
