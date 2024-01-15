#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AssetRipper.Primitives.Tests;

public class UnityGuidTests
{
	private const string randomGuidString = "352a5b3897136ed2702a283243520538";
	private const string sequentialGuidString = "0123456789abcdef0fedcba987654321";

	[Test]
	public void MissingReferenceSerializedCorrectly()
	{
		Assert.That(UnityGuid.MissingReference.ToString(), Is.EqualTo("0000000deadbeef15deadf00d0000000"));
	}

	[Test]
	public void ToByteArrayIsConsistentWithConstructorFromByteArray()
	{
		UnityGuid guid = UnityGuid.NewGuid();
		byte[] bytes = guid.ToByteArray();
		UnityGuid fromBytes = new UnityGuid(bytes);
		Assert.That(fromBytes, Is.EqualTo(guid));
		Assert.That(fromBytes.ToString(), Is.EqualTo(guid.ToString()));
	}

	[Test]
	public void ConversionFromSystemGuidToUnityGuidProducesSameString()
	{
		Guid systemGuid = Guid.NewGuid();
		UnityGuid unityGUID = new UnityGuid(systemGuid);
		Assert.That(unityGUID.ToString(), Is.EqualTo(systemGuid.ToString().Replace("-", "")));
	}

	[Test]
	public void IsZeroReturnsTrueForTheZeroGuid()
	{
		UnityGuid unityGUID = new UnityGuid(0, 0, 0, 0);
		Assert.That(unityGUID.IsZero, Is.True);
	}

	[Test]
	public void IsZeroReturnsFalseForRandomGuid()
	{
		UnityGuid unityGUID = UnityGuid.NewGuid();
		Assert.That(unityGUID.IsZero, Is.False);
	}

	[Test]
	public void ParsedGuidOutputsSameString()
	{
		UnityGuid parsedGUID = UnityGuid.Parse(randomGuidString);
		string outputedString = parsedGUID.ToString();
		Assert.That(outputedString, Is.EqualTo(randomGuidString));
	}

	[Test]
	public void ConversionsAreInverses()
	{
		UnityGuid unityGuid = UnityGuid.NewGuid();
		Guid systemGuid = (Guid)unityGuid;
		Assert.That((UnityGuid)systemGuid, Is.EqualTo(unityGuid));
	}

	[Test]
	public void ByteConversionIsItsOwnInverse()
	{
		UnityGuid originalGuid = UnityGuid.NewGuid();
		UnityGuid inverseGuid = new UnityGuid(originalGuid.ToByteArray());
		UnityGuid equivalentGuid = new UnityGuid(inverseGuid.ToByteArray());
		Assert.That(equivalentGuid, Is.EqualTo(originalGuid));
	}

	[Test]
	public void UnityGuidIsTheSameSizeAsSystemGuid()
	{
		Assert.That(Unsafe.SizeOf<UnityGuid>(), Is.EqualTo(Unsafe.SizeOf<Guid>()));
	}

	[Test]
	public void ToBytesMethod()
	{
		Guid originalGuid = Guid.NewGuid();
		UnityGuid tobyteArrayGuid = new UnityGuid(originalGuid.ToByteArray());
		UnityGuid memoryMarshallGuid = Unsafe.As<Guid, UnityGuid>(ref originalGuid);
		Assert.That(memoryMarshallGuid, Is.EqualTo(tobyteArrayGuid));
	}
}
#endif