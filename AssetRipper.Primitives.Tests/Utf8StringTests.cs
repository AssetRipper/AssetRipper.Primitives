#if NET7_0_OR_GREATER
namespace AssetRipper.Primitives.Tests;

public class Utf8StringTests
{
	[Test]
	public void Utf8StringEqualityWorks()
	{
		Utf8String utf8Str = new Utf8String("AssetRipperTestString");
		Utf8String utf8Str2 = new Utf8String(utf8Str.Data.ToArray());

		Assert.That(utf8Str2, Is.EqualTo(utf8Str));
	}

	[Test]
	public void StringEmptyIsEquivalentToUtf8StringEmpty()
	{
		Assert.Multiple(() =>
		{
			Assert.That(Utf8String.Empty.Data.Length, Is.EqualTo(0));
			Assert.That(Utf8String.Empty.String, Is.Empty);
#pragma warning disable NUnit2010 // Use EqualConstraint for better assertion messages in case of failure
			Assert.That(Utf8String.Empty == "");
			Assert.That("" == Utf8String.Empty);
#pragma warning restore NUnit2010 // Use EqualConstraint for better assertion messages in case of failure
			Assert.That(new Utf8String(""), Is.EqualTo(Utf8String.Empty));
		});
	}

	/// <summary>
	/// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-11.0/utf8-string-literals"/>
	/// </summary>
	[Test]
	public void Utf8StringMadeFromUtf8LiteralIsTheSameAsMadeFromConstantString()
	{
		Utf8String dataString = "AssetRipperTestString"u8;
		Utf8String systemString = "AssetRipperTestString";
		Assert.That(dataString, Is.EqualTo(systemString));
	}

	[Test]
	public void ConcatenationBetweenTwoUtf8StringsWorks()
	{
		Utf8String dataString = "AssetRipperTestString"u8;
		Utf8String systemString = "AssetRipperTestString";
		Utf8String concatenated = dataString + systemString;
		Assert.That(concatenated, Is.EqualTo((Utf8String)"AssetRipperTestStringAssetRipperTestString"u8));
	}

	[Test]
	public void ConcatenationBetweenThreeUtf8StringsWorksTheSameAsAddition()
	{
		Utf8String added = "String1"u8 + new Utf8String("2") + "3"u8;
		Utf8String concatenated = Utf8String.Concat("String1"u8, "2"u8, "3"u8);
		Assert.That(added, Is.EqualTo(concatenated));
	}

	[Test]
	public void ConcatenationBetweenFourUtf8StringsWorks()
	{
		Utf8String string1 = "hello"u8;
		Utf8String string2 = "world"u8;
		Utf8String string3 = "!"u8;
		Utf8String string4 = ", how are you?"u8;
		Utf8String concatenated = Utf8String.Concat(string1, string2, string3, string4);
		Assert.That(concatenated, Is.EqualTo((Utf8String)"hello world!, how are you?"u8));
	}

	[Test]
	public void IsNullOrEmptyReturnsTrueForEmptyString()
	{
		Assert.That(Utf8String.IsNullOrEmpty(Utf8String.Empty), Is.True);
	}

	[Test]
	public void IsNullOrEmptyReturnsTrueForNullString()
	{
		Assert.That(Utf8String.IsNullOrEmpty(null), Is.True);
	}

	[Test]
	public void IsNullOrEmptyReturnsFalseForNonEmptyString()
	{
		Assert.That(Utf8String.IsNullOrEmpty("AssetRipperTestString"u8), Is.False);
	}
}
#endif