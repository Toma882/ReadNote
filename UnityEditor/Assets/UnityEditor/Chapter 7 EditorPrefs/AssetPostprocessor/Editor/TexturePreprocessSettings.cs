using System.IO;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class TexturePreprocessSettings : ScriptableObject
{
    [SerializeField] private TextureImporterType textureType = TextureImporterType.Default;
    [SerializeField] private TextureImporterShape textureShape = TextureImporterShape.Texture2D;
    [SerializeField] private bool sRGBTexture = true;
    [SerializeField] private TextureImporterAlphaSource alphaSource = TextureImporterAlphaSource.FromInput;
    [SerializeField] private bool alphaIsTransparency;
    [SerializeField] private bool ignorePNGFileGamma;

    [Header("Advanced")]
    [SerializeField] private TextureImporterNPOTScale nonPowerOf2 = TextureImporterNPOTScale.ToNearest;
    [SerializeField] private bool readWriteEnabled;
    [SerializeField] private bool streamingMipmaps;
    [SerializeField] private bool vitrualTextureOnly;
    [SerializeField] private bool generateMipMaps = true;
    [SerializeField] private bool borderMipMaps;
    [SerializeField] private TextureImporterMipFilter mipmapFilter = TextureImporterMipFilter.BoxFilter;
    [SerializeField] private bool mipMapsPreserveCoverage;
    [SerializeField] private bool fadeoutMipMaps;

    [SerializeField] private TextureWrapMode wrapMode = TextureWrapMode.Repeat;
    [SerializeField] private FilterMode filterMode = FilterMode.Bilinear;
    [SerializeField, Range(0, 16)] private int anisoLevel = 1;

    [SerializeField] private int maxSize = 2048;
    [SerializeField] private TextureImporterFormat format = TextureImporterFormat.Automatic;
    [SerializeField] private TextureImporterCompression compression = TextureImporterCompression.Compressed;
    [SerializeField] private bool useCrunchCompression;

    private static TexturePreprocessSettings m_Settings;
    private static TexturePreprocessSettings Settings
    {
        get
        {
            if (m_Settings == null)
            {
                var path = "Assets/Settings/Texture Preprocess Settings.asset";
                m_Settings = AssetDatabase.LoadAssetAtPath<TexturePreprocessSettings>(path);
                if (m_Settings == null)
                {
                    m_Settings = CreateInstance<TexturePreprocessSettings>();
                    var directory = Application.dataPath + "/Settings";
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(Application.dataPath + "/Settings");
                    AssetDatabase.CreateAsset(m_Settings, path);
                    AssetDatabase.Refresh();
                }
            }
            return m_Settings;
        }
    }

    public static TextureImporterType TextureType => Settings.textureType;
    public static TextureImporterShape TextureShape => Settings.textureShape;
    public static bool SRGBTexture => Settings.sRGBTexture;
    public static TextureImporterAlphaSource AlphaSource => Settings.alphaSource;
    public static bool AlphaIsTransparency => Settings.alphaIsTransparency;
    public static bool IgnorePNGFileGamma => Settings.ignorePNGFileGamma;

    public static TextureImporterNPOTScale NonPowerOf2 => Settings.nonPowerOf2;
    public static bool ReadWriteEnabled => Settings.readWriteEnabled;
    public static bool StreamingMipmaps => Settings.streamingMipmaps;
    public static bool VitrualTextureOnly => Settings.vitrualTextureOnly;
    public static bool GenerateMipMaps => Settings.generateMipMaps;
    public static bool BorderMipMaps => Settings.borderMipMaps;
    public static TextureImporterMipFilter MipmapFilter => Settings.mipmapFilter;
    public static bool MipMapsPreserveCoverage => Settings.mipMapsPreserveCoverage;
    public static bool FadeoutMipMaps => Settings.fadeoutMipMaps;

    public static TextureWrapMode WrapMode => Settings.wrapMode;
    public static FilterMode FilterMode => Settings.filterMode;
    public static int AnisoLevel => Settings.anisoLevel;

    public static int MaxSize => Settings.maxSize;
    public static TextureImporterFormat Format => Settings.format;
    public static TextureImporterCompression Compression => Settings.compression;
    public static bool UseCrunchCompression => Settings.useCrunchCompression;
}