using DotPrimitives.IO.Drives;

namespace DotPrimitives.Tests.IO.Drives;

public class StorageDriveDetectorTests
{
    private readonly IStorageDriveDetector _storageDriveDetector;

    public StorageDriveDetectorTests()
    {
        _storageDriveDetector = StorageDrives.Shared;
    }
    
    [Test]
    public async Task LogicalDrives_NotEmpty()
    {
        IEnumerable<DriveInfo> actual = _storageDriveDetector.EnumerateLogicalDrives();

        await Assert.That(actual)
            .IsNotEmpty();
    }
    
    [Test]
    public async Task PhysicalDrives_NotEmpty()
    {
        IEnumerable<DriveInfo> actual = _storageDriveDetector.EnumeratePhysicalDrives();

        await Assert.That(actual)
            .IsNotEmpty();
    }
}