#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET461_OR_GREATER
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AssetRipper.Primitives;

/// <summary>
/// An immutable, thread-safe, representation of UTF8 text data.
/// </summary>
/// <remarks>
/// Conversions to and from <see cref="string"/> are handled by <see cref="Encoding.UTF8"/>.
/// </remarks>
public sealed class Utf8String : IEquatable<Utf8String>
{
	private readonly byte[] data;
	private string? cachedString;

	public static Utf8String Empty { get; } = new();

	/// <summary>
	/// The empty string. A shared instance is available in <see cref="Empty"/>.
	/// </summary>
	public Utf8String()
	{
		data = Array.Empty<byte>();
		cachedString = "";
	}

	public Utf8String(ReadOnlySpan<byte> data) : this(data.ToArray())
	{
	}

	private Utf8String(byte[] data)
	{
		this.data = data;
	}

	public Utf8String(string @string)
	{
		cachedString = @string;
		data = Encoding.UTF8.GetBytes(@string);
	}

	public ReadOnlySpan<byte> Data => data;

	public string String
	{
		get
		{
			cachedString ??= Encoding.UTF8.GetString(data);
			return cachedString;
		}
	}

	public bool IsEmpty => data.Length == 0;

	public override string ToString() => String;

	public override int GetHashCode()
	{
		HashCode hash = new();
#if NET6_0_OR_GREATER
		hash.AddBytes(data);
#else
		foreach (byte b in data)
		{
			hash.Add(b);
		}
#endif
		return hash.ToHashCode();
	}

	public override bool Equals(object? obj) => Equals(obj as Utf8String);

	public bool Equals(Utf8String? other) => other is not null && Data.SequenceEqual(other.Data);

	public static bool operator ==(Utf8String? string1, Utf8String? string2)
	{
		return EqualityComparer<Utf8String>.Default.Equals(string1, string2);
	}
	public static bool operator !=(Utf8String? string1, Utf8String? string2) => !(string1 == string2);

	public static bool operator ==(Utf8String? utf8String, string? @string) => utf8String?.String == @string;
	public static bool operator !=(Utf8String? utf8String, string? @string) => utf8String?.String != @string;

	public static bool operator ==(string? @string, Utf8String? utf8String) => utf8String?.String == @string;
	public static bool operator !=(string? @string, Utf8String? utf8String) => utf8String?.String != @string;

	[return: NotNullIfNotNull(nameof(@string))]
	public static implicit operator Utf8String?(string? @string)
	{
		return @string is null
			? null
			: @string.Length == 0
				? Empty
				: new Utf8String(@string);
	}

	[return: NotNullIfNotNull(nameof(utf8String))]
	public static implicit operator string?(Utf8String? utf8String)
	{
		return utf8String?.String;
	}

	public static implicit operator Utf8String(ReadOnlySpan<byte> data)
	{
		return data.Length == 0 ? Empty : new Utf8String(data);
	}

	public static implicit operator ReadOnlySpan<byte>(Utf8String? utf8String)
	{
		return utf8String is null ? default : utf8String.Data;
	}

	public static Utf8String operator +(Utf8String? string1, Utf8String? string2)
	{
		return Concat(string1, string2);
	}

	public static Utf8String operator +(Utf8String? string1, ReadOnlySpan<byte> string2)
	{
		return string1 is null
			? string2.Length == 0 ? Empty : new Utf8String(string2)
			: Concat(string1.Data, string2);
	}

	public static Utf8String operator +(ReadOnlySpan<byte> string1, Utf8String? string2)
	{
		return string2 is null
			? string1.Length == 0 ? Empty : new Utf8String(string1)
			: Concat(string1, string2.Data);
	}

	public static bool IsNullOrEmpty([NotNullWhen(false)] Utf8String? utf8String)
	{
		return utf8String is null || utf8String.IsEmpty;
	}

	public static Utf8String Concat(Utf8String? string1, Utf8String? string2)
	{
		if (IsNullOrEmpty(string1))
		{
			return string2 ?? Empty;
		}
		if (IsNullOrEmpty(string2))
		{
			return string1;
		}

		byte[] data = new byte[string1.data.Length + string2.data.Length];
		string1.data.CopyTo(data.AsSpan());
		string2.data.CopyTo(data.AsSpan(string1.data.Length));
		return new Utf8String(data);
	}

	public static Utf8String Concat(Utf8String? string1, Utf8String? string2, Utf8String? string3)
	{
		if (IsNullOrEmpty(string1))
		{
			return Concat(string2, string3);
		}
		if (IsNullOrEmpty(string2))
		{
			return Concat(string1, string3);
		}
		if (IsNullOrEmpty(string3))
		{
			return Concat(string1, string2);
		}

		byte[] data = new byte[string1.data.Length + string2.data.Length + string3.data.Length];
		string1.data.CopyTo(data.AsSpan());
		string2.data.CopyTo(data.AsSpan(string1.data.Length));
		string3.data.CopyTo(data.AsSpan(string1.data.Length + string2.data.Length));
		return new Utf8String(data);
	}

	public static Utf8String Concat(Utf8String? string1, Utf8String? string2, Utf8String? string3, Utf8String? string4)
	{
		if (IsNullOrEmpty(string1))
		{
			return Concat(string2, string3, string4);
		}
		if (IsNullOrEmpty(string2))
		{
			return Concat(string1, string3, string4);
		}
		if (IsNullOrEmpty(string3))
		{
			return Concat(string1, string2, string4);
		}
		if (IsNullOrEmpty(string4))
		{
			return Concat(string1, string2, string3);
		}

		byte[] data = new byte[string1.data.Length + string2.data.Length + string3.data.Length + string4.data.Length];
		string1.data.CopyTo(data.AsSpan());
		string2.data.CopyTo(data.AsSpan(string1.data.Length));
		string3.data.CopyTo(data.AsSpan(string1.data.Length + string2.data.Length));
		string4.data.CopyTo(data.AsSpan(string1.data.Length + string2.data.Length + string3.data.Length));
		return new Utf8String(data);
	}

	public static Utf8String Concat(params Utf8String?[] values)
	{
		if (values is null)
		{
			throw new ArgumentNullException(nameof(values));
		}

		int totalLength = 0;
		foreach (Utf8String? value in values)
		{
			if (value is null)
			{
				continue;
			}

			totalLength += value.data.Length;
		}

		if (totalLength == 0)
		{
			return Empty;
		}

		byte[] data = new byte[totalLength];
		int offset = 0;
		foreach (Utf8String? value in values)
		{
			if (value is null)
			{
				continue;
			}

			value.data.CopyTo(data.AsSpan(offset));
			offset += value.data.Length;
		}

		return new Utf8String(data);
	}

	public static Utf8String Concat(ReadOnlySpan<byte> string1, ReadOnlySpan<byte> string2)
	{
		if (string1.Length == 0)
		{
			return string2.Length == 0 ? Empty : new Utf8String(string2);
		}
		if (string2.Length == 0)
		{
			return new Utf8String(string1);
		}

		byte[] data = new byte[string1.Length + string2.Length];
		string1.CopyTo(data);
		string2.CopyTo(data.AsSpan(string1.Length));
		return new Utf8String(data);
	}

	public static Utf8String Concat(ReadOnlySpan<byte> string1, ReadOnlySpan<byte> string2, ReadOnlySpan<byte> string3)
	{
		if (string1.Length == 0)
		{
			return Concat(string2, string3);
		}
		if (string2.Length == 0)
		{
			return Concat(string1, string3);
		}
		if (string3.Length == 0)
		{
			return Concat(string1, string2);
		}

		byte[] data = new byte[string1.Length + string2.Length + string3.Length];
		string1.CopyTo(data);
		string2.CopyTo(data.AsSpan(string1.Length));
		string3.CopyTo(data.AsSpan(string1.Length + string2.Length));
		return new Utf8String(data);
	}

	public static Utf8String Concat(ReadOnlySpan<byte> string1, ReadOnlySpan<byte> string2, ReadOnlySpan<byte> string3, ReadOnlySpan<byte> string4)
	{
		if (string1.Length == 0)
		{
			return Concat(string2, string3, string4);
		}
		if (string2.Length == 0)
		{
			return Concat(string1, string3, string4);
		}
		if (string3.Length == 0)
		{
			return Concat(string1, string2, string4);
		}
		if (string4.Length == 0)
		{
			return Concat(string1, string2, string3);
		}

		byte[] data = new byte[string1.Length + string2.Length + string3.Length + string4.Length];
		string1.CopyTo(data);
		string2.CopyTo(data.AsSpan(string1.Length));
		string3.CopyTo(data.AsSpan(string1.Length + string2.Length));
		string4.CopyTo(data.AsSpan(string1.Length + string2.Length + string3.Length));
		return new Utf8String(data);
	}
}
#endif