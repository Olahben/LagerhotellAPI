﻿using LagerhotellAPI.Models;
using LagerhotellAPI.Models.DomainModels.Validators;
using MongoDB.Driver;

namespace LagerhotellAPI.Services;

public class AssetService
{
    private readonly IMongoCollection<Models.DbModels.ImageAsset> _assets;
    private readonly ImageAssetValidator _imageAssetValidator = new();
    private readonly WarehouseHotelService _warehouseHotelService;
    public AssetService(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase("Lagerhotell");
        _assets = database.GetCollection<Models.DbModels.ImageAsset>("Assets");
        _warehouseHotelService = new WarehouseHotelService(settings);
    }

    /// <summary>
    /// Generates a new assetId and adds the asset to the database
    /// </summary>
    /// <param name="asset"></param>
    /// <returns>string of the assetId</returns>
    public async Task<string> AddAsset(ImageAsset asset)
    {
        if (await GetAsset(asset.AssetId) != null)
        {
            throw new InvalidOperationException("Asset already exists");
        }
        string assetId = Guid.NewGuid().ToString();
        Models.DbModels.ImageAsset dbAsset = new() { AssetId = assetId, Name = asset.Name, Tags = asset.Tags, ImageBytes = asset.ImageBytes, WarehouseHotelId = asset.WarehouseHotelId };
        await _assets.InsertOneAsync(dbAsset);
        return assetId;
    }

    public async Task DeleteAsset(string assetId)
    {
        if (await GetAsset(assetId) == null)
        {
            throw new KeyNotFoundException("Asset not found");
        }
        await _assets.DeleteOneAsync(asset => asset.AssetId == assetId);
    }

    /// <summary>
    /// Modifies an asset but keeps the Id
    /// </summary>
    /// <param name="assetId"></param>
    /// <param name="updatedAsset"></param>
    /// <returns></returns>
    public async Task ModifyAsset(string assetId, ImageAsset updatedAsset)
    {
        if (await GetAsset(assetId) == null)
        {
            throw new KeyNotFoundException("Asset not found");
        }
        Models.DbModels.ImageAsset updatedDbAsset = new() { AssetId = assetId, Name = updatedAsset.Name, Tags = updatedAsset.Tags, ImageBytes = updatedAsset.ImageBytes, WarehouseHotelId = updatedAsset.WarehouseHotelId };
        await _assets.ReplaceOneAsync(asset => asset.AssetId == assetId, updatedDbAsset);
    }

    public async Task<ImageAsset?> GetAsset(string assetId)
    {
        var dbAsset = await _assets.Find(asset => asset.AssetId == assetId).FirstOrDefaultAsync();
        if (dbAsset == null)
        {
            return null;
        }
        return new ImageAsset() { AssetId = dbAsset.AssetId, Name = dbAsset.Name, Tags = dbAsset.Tags, ImageBytes = dbAsset.ImageBytes, WarehouseHotelId = dbAsset.WarehouseHotelId };
    }

    public async Task<List<ImageAsset>> GetAssets(int? skip, int? take, string? warehouseHotelId)
    {
        List<Models.DbModels.ImageAsset> dbAssets;
        if (warehouseHotelId != null)
        {
            if (_warehouseHotelService.GetWarehouseHotelById(warehouseHotelId) == null)
            {
                throw new KeyNotFoundException("Warehouse hotel not found");
            }
            dbAssets = await _assets.Find(asset => asset.WarehouseHotelId == warehouseHotelId).Skip(skip).Limit(take).ToListAsync();
        }
        else
        {
            dbAssets = await _assets.Find(asset => true).Skip(skip).Limit(take).ToListAsync();
        }
        return dbAssets.Select(asset => new ImageAsset() { AssetId = asset.AssetId, Name = asset.Name, Tags = asset.Tags, ImageBytes = asset.ImageBytes, WarehouseHotelId = asset.WarehouseHotelId }).ToList();
    }
}
