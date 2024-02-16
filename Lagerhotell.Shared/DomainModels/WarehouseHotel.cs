﻿namespace LagerhotellAPI.Models.DomainModels;

public class WarehouseHotel
{
    public string? WarehouseHotelId { get; set; }
    public Coordinate Coordinate { get; set; }
    public Address Address { get; set; }
    public string Name { get; set; }
    public OpeningHours OpeningHours { get; set; }
    public string PhoneNumber { get; set; }
    public string DetailedDescription { get; set; }
    public bool ContainsTemperatedStorageUnits { get; set; }
    public bool IsActive { get; set; }
    public StorageUnitSizes StorageUnitsSizes { get; set; }

    public WarehouseHotel(Coordinate coordinate, Address address, string name, OpeningHours openingHours, string phoneNumber, string detailedDescription, bool containsTemperatedStorageUnits, bool isActive, StorageUnitSizes storageUnitSizes)
    {
        Coordinate = coordinate;
        Address = address;
        Name = name;
        OpeningHours = openingHours;
        PhoneNumber = phoneNumber;
        DetailedDescription = detailedDescription;
        ContainsTemperatedStorageUnits = containsTemperatedStorageUnits;
        IsActive = isActive;
        StorageUnitsSizes = storageUnitSizes;
    }
    public WarehouseHotel(string warehouseHotelId, Coordinate coordinate, Address address, string name, OpeningHours openingHours, string phoneNumber, string detailedDescription, bool containsTemperatedStorageUnits, bool isActive, StorageUnitSizes storageUnitSizes)
    {
        WarehouseHotelId = warehouseHotelId;
        Coordinate = coordinate;
        Address = address;
        Name = name;
        OpeningHours = openingHours;
        PhoneNumber = phoneNumber;
        DetailedDescription = detailedDescription;
        ContainsTemperatedStorageUnits = containsTemperatedStorageUnits;
        IsActive = isActive;
        StorageUnitsSizes = storageUnitSizes;
    }

    public WarehouseHotel()
    {
        // Parameterless constructor
    }
}
