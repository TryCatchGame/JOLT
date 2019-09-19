using System.Collections.Generic;
using UnityEngine;
using GameInterface.ShopMenu;

using MyBox;
using System.IO;
using System.Text;
namespace GameManager.CoreSkin {
    public class CoreSkinDataManager : Singleton<CoreSkinDataManager> {
        private static string DATA_FILE_PATH;

        #region SkinDataCollection_STRUCT
        [System.Serializable]
        private struct SkinDataCollection {
            public CoreSkinData[] skinDatas;
        }
        #endregion

        [Separator()]
        [SerializeField, Tooltip("The sprite of the default skin core"), MustBeAssigned]
        private Sprite defaultCoreSprite;

        [SerializeField, Tooltip("The sprite of all the skin cores"), MustBeAssigned]
        private Sprite[] skinCoreSprites;

        internal Sprite DefaultCoreSkin { get => defaultCoreSprite; }

        private void Awake() {
            DATA_FILE_PATH = Path.Combine(Application.dataPath, "skinData.dat");
            InitalizeDataFileIfNotExists();

            DontDestroyOnLoad(gameObject);
            Debug.Log("CoreSkinDataManager Initalized!"); // DEBUG!!!

            #region Local_Function
            void InitalizeDataFileIfNotExists() {
                if (!File.Exists(DATA_FILE_PATH)) {
                    using (FileStream fileStream = File.Create(DATA_FILE_PATH)) {
                        var template = CreateTemplateCollection();

                        string templateData = JsonUtility.ToJson(template);
                        byte[] bytes = Encoding.ASCII.GetBytes(templateData);

                        fileStream.Write(bytes, 0, bytes.Length);
                    }
                }
            }

            SkinDataCollection CreateTemplateCollection() {
                GlobalProperties.CurrentlyUsingSkinName_Local = defaultCoreSprite.name;

                List<CoreSkinData> templateDatas = new List<CoreSkinData> {
                    new CoreSkinData(defaultCoreSprite.name, ShopItemState.IN_USE)
                };

                foreach (var sprite in skinCoreSprites) {
                    templateDatas.Add(new CoreSkinData(sprite.name, ShopItemState.NORMAL));
                }

                return new SkinDataCollection {
                    skinDatas = templateDatas.ToArray()
                };
            }
            #endregion
        }

        internal static void SaveCoreSkinData(CoreSkinData[] skinDatas) {
            SkinDataCollection collectionToSave = new SkinDataCollection {
                skinDatas = skinDatas
            };

            string dataToSave = JsonUtility.ToJson(collectionToSave);
            byte[] bytes = Encoding.ASCII.GetBytes(dataToSave);

            File.WriteAllBytes(DATA_FILE_PATH, bytes);
        }

        internal static CoreSkinData[] LoadCoreSkinData() {
            try {
                byte[] bytes = File.ReadAllBytes(DATA_FILE_PATH);
                string savedData = Encoding.ASCII.GetString(bytes);

                return JsonUtility.FromJson<SkinDataCollection>(savedData).skinDatas;
            } catch (System.Exception) {
                return new CoreSkinData[0];
            }
        }

        internal static CoreSkinData[] GetCoreSkinDataByCondition(System.Func<CoreSkinData, bool> condition) {
            CoreSkinData[] allCoreSkinData = LoadCoreSkinData();
            List<CoreSkinData> filteredData = new List<CoreSkinData>();

            foreach (var coreSkinData in allCoreSkinData) {
                if (condition(coreSkinData)) {
                    filteredData.Add(coreSkinData);
                }
            }

            return filteredData.ToArray();
        }

        internal static void OverrideCoreSkinData(CoreSkinData[] dataToOverride) {
            CoreSkinData[] currentCoreSkinData = LoadCoreSkinData();

            for (int i = 0; i < currentCoreSkinData.Length; ++i) {
                foreach (var overridingData in dataToOverride) {
                    if (currentCoreSkinData[i].skinName == overridingData.skinName) {
                        currentCoreSkinData[i].skinState = overridingData.skinState;
                    }
                }
            }

            SaveCoreSkinData(currentCoreSkinData);
        }

        internal Sprite[] GetAllSprites() {
            return skinCoreSprites.AddElement(defaultCoreSprite);
        }

        internal bool TryGetSpriteBySpriteName(string spriteName, out Sprite sprite) {
            sprite = null;

            if (spriteName == defaultCoreSprite.name) {
                sprite = defaultCoreSprite;
                return true;
            }

            foreach (var coreSprite in skinCoreSprites) {
                if (coreSprite.name == spriteName) {
                    sprite = coreSprite;
                    break;
                }
            }

            return sprite != null;
        }
    }
}