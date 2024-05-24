using System.Security.Cryptography;

namespace seeder;
public class AES
{
    public static readonly int BlockSize = 16;

    public static byte[] Encrypt(byte[] plainText, byte[] key, byte[] iv)
    {
        int blockSize = BlockSize;
        byte[] previousBlock = iv;
        byte[] encrypted = new byte[plainText.Length];

        for (int i = 0; i < plainText.Length; i += blockSize)
        {
            byte[] block = plainText.Skip(i).Take(blockSize).ToArray();
            byte[] xoredBlock = XorBlocks(block, previousBlock);
            byte[] encryptedBlock = EncryptBlock(xoredBlock, key);
            Array.Copy(encryptedBlock, 0, encrypted, i, blockSize);
            previousBlock = encryptedBlock;
        }

        return encrypted;
    }

    public static byte[] Decrypt(byte[] cipherText, byte[] key, byte[] iv)
    {
        int blockSize = BlockSize;
        byte[] previousBlock = iv;
        byte[] decrypted = new byte[cipherText.Length];

        for (int i = 0; i < cipherText.Length; i += blockSize)
        {
            byte[] block = cipherText.Skip(i).Take(blockSize).ToArray();
            byte[] decryptedBlock = DecryptBlock(block, key);
            byte[] xoredBlock = XorBlocks(decryptedBlock, previousBlock);
            Array.Copy(xoredBlock, 0, decrypted, i, blockSize);
            previousBlock = block;
        }

        return Unpad(decrypted);
    }

    public static byte[] EncryptBlock(byte[] block, byte[] key)
    {
        // Simplified substitution and permutation (not real AES)
        byte[] substitutedBlock = SubstituteBytes(block);
        byte[] permutedBlock = PermuteBytes(substitutedBlock);
        return permutedBlock;
    }

    public static byte[] DecryptBlock(byte[] block, byte[] key)
    {
        // Inverse permutation and substitution
        byte[] inversePermutedBlock = PermuteBytes(block);
        byte[] inverseSubstitutedBlock = SubstituteBytes(inversePermutedBlock);
        return Unpad(inverseSubstitutedBlock);
    }

    public static byte[] SubstituteBytes(byte[] block)
    {
        // Simple substitution (not real AES S-Box)
        return block.Select(b => (byte)(b ^ 0xA5)).ToArray();
    }

    public static byte[] PermuteBytes(byte[] block)
    {
        // Simple permutation (not real AES permutation)
        return block.Reverse().ToArray();
    }

    public static byte[] XorBlocks(byte[] block1, byte[] block2)
    {
        byte[] xoredBlock = new byte[block1.Length];
        for (int i = 0; i < block1.Length; i++)
        {
            xoredBlock[i] = (byte)(block1[i] ^ block2[i]);
        }
        return xoredBlock;
    }

    public static byte[] Pad(byte[] plainText, int blockSize)
    {
        int paddingSize = blockSize - (plainText.Length % blockSize);
        byte[] paddedPlainText = new byte[plainText.Length + paddingSize];
        Array.Copy(plainText, paddedPlainText, plainText.Length);
        for (int i = plainText.Length; i < paddedPlainText.Length; i++)
        {
            paddedPlainText[i] = (byte)paddingSize;
        }
        return paddedPlainText;
    }

    public static byte[] Unpad(byte[] paddedPlainText)
    {
        int paddingSize = paddedPlainText[paddedPlainText.Length - 1];
        byte[] plainText = new byte[paddedPlainText.Length - paddingSize];
        Array.Copy(paddedPlainText, plainText, plainText.Length);
        return plainText;
    }

    public static byte[] GenerateRandomBytes(int size)
    {
        byte[] randomBytes = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }
        return randomBytes;
    }
}
