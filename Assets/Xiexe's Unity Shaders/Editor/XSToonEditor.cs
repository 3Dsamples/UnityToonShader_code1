using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class XSToonEditor : ShaderGUI
{
    public enum BlendMode
    {
        Opaque,
        Cutout
    }

    public enum DisplayType
    {
        Basic,
        Advanced
    }


    // Styles for most options on the texture - shows name and mouseover description
    //public static GUIContent nameText = new GUIContent("name", "desc");
    private static class Styles
    {
        public static GUIContent mainText = new GUIContent("Main Options", "The main options for the Shader");
        public static GUIContent rampText = new GUIContent("Shadow Ramp", "Shadow ramp texture, horizontal or vertical, Black to White gradient that controls how shadows look - examples are included in the Shadow Ramps folder");
        public static GUIContent specMapText = new GUIContent("Specular Map", "Specular Map, This controls where the specular pattern can show. Should be a black and white image");
        public static GUIContent specPatternText = new GUIContent("Specular Pattern", "Specular Pattern, This is the pattern that shows in specular reflections. Control how much shows with the Smoothness and Intensity");
        public static GUIContent MainTexText = new GUIContent("Main Texture", "Main texture (RGB) and Transparency (A)");
        public static GUIContent normalText = new GUIContent("Normal Map", "Normal Map, used for controlling how light bends to fake small details, such as a cloth pattern");
        public static GUIContent emissText = new GUIContent("Emissive Map", "The texture used to control where emission happens, I recommend a black and white image, white for on, black for off");
        public static GUIContent simLightText = new GUIContent("Light Direction", "The fake lighting direction. This will only show if you disable all realtime lights in your scene. The shader detects if there is no realtime light, and uses this as a fake direction for lighting automatically");
        public static GUIContent SmoothnessText = new GUIContent("Glossiness", "How smooth the material is - this affects the size of the area that the pattern shows when reflecting light");
        public static GUIContent sintensityText = new GUIContent("Intensity", "The intensity of the specular reflections, higher is more visible");
        public static GUIContent stilingText = new GUIContent("Tiling", "The tiling of the specular reflection's pattern, used to make the pattern smaller or larger");
        public static GUIContent rimWidthText = new GUIContent("Rim Width", "The width of the rimlight, there is no catch-all value - you will probably need to figure out what works on a per model basis");
        public static GUIContent rimIntText = new GUIContent("Rim Intensity", "The intensity of the rimlight, this is how bright this rimlight is in comparison to the main texture");
        public static GUIContent rimLightTypeText = new GUIContent("Rim Style", "The style of rimlight, which is an edge light around the model in the lit up areas, sharp or smooth.");
        public static GUIContent cullingModeText = new GUIContent("Culling Mode", "Changes which side of the mesh is visible. Off is two-sided.");
        public static GUIContent cutoutText = new GUIContent("Cutout Amount", "This option only works on the 'Cutout' varient of the shader, and will do nothing on the others.");
        public static GUIContent advancedOptions = new GUIContent("Advanced Options", "This is where advanced options will go, anything that isn't part of the base experience, they are not for the faint of heart. Don't break anything :)");
        public static GUIContent MetalMap = new GUIContent("Metallic Map", "Black to white texture that defines areas that can be metallic, full white = full metallic, full black = no metallic, if you use this, set Metallic to 0");
        public static GUIContent roughMap = new GUIContent("Roughness Map", "Black to white texture that defines the roughness of the object white = 100% rough, black = 100% smooth. If you use this, set Roughness to 1");
        public static GUIContent bakedCube = new GUIContent("Baked Cubemap", "This is the cubemap that will be sampled for reflections if there are no reflection probes in the scene, if there are, the shader will sampler those instead.");
        public static GUIContent shadowTypeText = new GUIContent("Recieved Shadows", "Received Realtime Shadow style, sharp or smooth, match this up with your shadow ramp for optimal results.");
        public static GUIContent ReflMask = new GUIContent("Reflection Mask", "Mask for reflections, the same as the metallic mask, black to white image.");
        public static GUIContent StyleIntensity = new GUIContent("Intensity", "The intensity of the stylized reflection.");
        public static GUIContent Saturation = new GUIContent("Saturation", "Saturation of the main texture.");
        public static GUIContent Matcap = new GUIContent("Matcap Texture", "A matcap texture. These generally look like orbs with some sort of lighting on them. You can find some in 'Textures > Matcap' as examples.");
        public static GUIContent normalTiling = new GUIContent("Tiling", "Normal map tiling, adjust the X and Y to make the normals larger or smaller.");
        public static GUIContent MatcapCubemap = new GUIContent("Cubemap", "A Cubemap. This can be made by selecting the texture and changing the type to 'cubemap' in the inspector. You may also want to change 'Convolution' to be 'Glossy Reflection' as well");
        public static GUIContent MatcapMask = new GUIContent("Mask", "The mask for the matcap. Black for off, white for on.");
        public static GUIContent detailNormal = new GUIContent("Detail Normal", "Detail Normals. These get blended on top of your regular normal for upclose detailing.");
        public static GUIContent detailMask = new GUIContent("Detail Mask", "Detail normal mask. Black to white, white = area with setail normals, black = area without.");
        public static GUIContent occlusionMap = new GUIContent("Occlusion Map", "Occlusion map. Used to bake shadowing into areas by removing Ambient Color. Black to White texture.");
        public static GUIContent thicknessMap = new GUIContent("Thickness Map", "Used to show 'Thickness' in an area by stopping light from coming through. Black to white texture, Black means less light comes through.");
    }

    void DoFooter()
    {
        GUILayout.Label(XSStyles.Styles.version, new GUIStyle(EditorStyles.centeredGreyMiniLabel)
        {
            alignment = TextAnchor.MiddleCenter,
            wordWrap = true,
            fontSize = 12
        });

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        XSStyles.discordButton(20, 20);
        GUILayout.Space(2);
        XSStyles.patreonButton(20, 20);
        XSStyles.githubButton(20, 20);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    MaterialProperty shadowRamp;
    MaterialProperty specMap;
    MaterialProperty specPattern;
    MaterialProperty tint;
    MaterialProperty mainTex;
    MaterialProperty normal;
    MaterialProperty specIntensity;
    MaterialProperty specArea;
    MaterialProperty rimWidth;
    MaterialProperty rimIntensity;
    MaterialProperty emissiveToggle;
    MaterialProperty emissiveTex;
    MaterialProperty emissiveColor;
    MaterialProperty advMode;
    MaterialProperty alphaCutoff;
    MaterialProperty culling;
    MaterialProperty rimStyle;
    MaterialProperty uv2;
    MaterialEditor m_MaterialEditor;
    MaterialProperty colorMask;
    MaterialProperty stencil;
    MaterialProperty stencilComp;
    MaterialProperty stencilOp;
    MaterialProperty stencilFail;
    MaterialProperty stencilZFail;
    MaterialProperty ztest;
    MaterialProperty zwrite;
    MaterialProperty reflSmooth;
    MaterialProperty metal;
    MaterialProperty metalMap;
    MaterialProperty roughMap;
    MaterialProperty bakedCube;
    MaterialProperty useRefl;
    MaterialProperty shadowType;
    MaterialProperty reflType;
    MaterialProperty saturation;
    MaterialProperty matcapStyle;
    MaterialProperty stylizedType;
    MaterialProperty rampColor;
    MaterialProperty rimColor;
    MaterialProperty aX;
    MaterialProperty aY;
    MaterialProperty specStyle;
    MaterialProperty detailNormal;
    MaterialProperty detailMask;
    MaterialProperty normalStrength;
	MaterialProperty detailNormalStrength;
    MaterialProperty occlusionMap;
    MaterialProperty occlusionStrength;
    MaterialProperty ThicknessMap;
    MaterialProperty SSSDist;
	MaterialProperty SSSPow;
    MaterialProperty SSSCol;
	MaterialProperty SSSIntensity;
	MaterialProperty invertThickness;
	MaterialProperty ThicknessMapPower;
    MaterialProperty UseSSS;
    MaterialProperty UseSpecular;
	MaterialProperty RampBaseAnchor;
    MaterialProperty UseUV2Emiss;
    MaterialProperty EmissScaleWithLight;
    MaterialProperty EmissTintToColor;
    MaterialProperty EmissionPower;

    public Texture ramp;

    //help buttons for editor
    public static GUISkin _xsSkin;
    public static string uiPath;
    bool showHelp = false;
    private float oldSpec;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
    {

        Material material = materialEditor.target as Material;
        {
            //Find all the properties within the shader
            shadowRamp = ShaderGUI.FindProperty("_ShadowRamp", props);
            specMap = ShaderGUI.FindProperty("_SpecularMap", props);
            specPattern = ShaderGUI.FindProperty("_SpecularPattern", props);
            tint = ShaderGUI.FindProperty("_Color", props);
            mainTex = ShaderGUI.FindProperty("_MainTex", props);
            normal = ShaderGUI.FindProperty("_Normal", props);
            specIntensity = ShaderGUI.FindProperty("_SpecularIntensity", props);
            specArea = ShaderGUI.FindProperty("_SpecularArea", props);
            rimWidth = ShaderGUI.FindProperty("_RimWidth", props);
            rimIntensity = ShaderGUI.FindProperty("_RimIntensity", props);
            emissiveToggle = ShaderGUI.FindProperty("_Emissive", props);
            emissiveTex = ShaderGUI.FindProperty("_EmissiveTex", props);
            emissiveColor = ShaderGUI.FindProperty("_EmissiveColor", props);
            alphaCutoff = ShaderGUI.FindProperty("_Cutoff", props);
            culling = ShaderGUI.FindProperty("_Culling", props);
            rimStyle = ShaderGUI.FindProperty("_RimlightType", props);
            uv2 = ShaderGUI.FindProperty("_UseUV2forNormalsSpecular", props);
            advMode = ShaderGUI.FindProperty("_advMode", props);
            reflSmooth = ShaderGUI.FindProperty("_ReflSmoothness", props);
            metal = ShaderGUI.FindProperty("_Metallic", props);
            metalMap = ShaderGUI.FindProperty("_MetallicMap", props);
            roughMap = ShaderGUI.FindProperty("_RoughMap", props);
            bakedCube = ShaderGUI.FindProperty("_BakedCube", props);
            shadowType = ShaderGUI.FindProperty("_ShadowType", props);
            reflType = ShaderGUI.FindProperty("_ReflType", props);
            saturation = ShaderGUI.FindProperty("_Saturation", props);
            useRefl = ShaderGUI.FindProperty("_UseReflections", props);
            matcapStyle = ShaderGUI.FindProperty("_MatcapStyle", props);
            stylizedType = ShaderGUI.FindProperty("_StylizedReflStyle", props);
            rampColor = ShaderGUI.FindProperty("_RampColor", props);
            rimColor = ShaderGUI.FindProperty("_RimColor", props);
            aX = ShaderGUI.FindProperty("_anistropicAX", props);
            aY = ShaderGUI.FindProperty("_anistropicAY", props);
            specStyle = ShaderGUI.FindProperty("_SpecularStyle", props);
            detailNormal = ShaderGUI.FindProperty("_DetailNormal", props);
            detailMask = ShaderGUI.FindProperty("_DetailMask", props);
            normalStrength = ShaderGUI.FindProperty("_NormalStrength", props);
            detailNormalStrength = ShaderGUI.FindProperty("_DetailNormalStrength", props);
            occlusionMap = ShaderGUI.FindProperty("_OcclusionMap", props);
            occlusionStrength = ShaderGUI.FindProperty("_OcclusionStrength", props);
            ThicknessMap = ShaderGUI.FindProperty("_ThicknessMap", props); 
            SSSDist = ShaderGUI.FindProperty("_SSSDist", props);
            SSSPow = ShaderGUI.FindProperty("_SSSPow", props);
            SSSIntensity = ShaderGUI.FindProperty("_SSSIntensity", props);
            SSSCol = ShaderGUI.FindProperty("_SSSCol", props);
            invertThickness = ShaderGUI.FindProperty("_invertThickness", props);
            ThicknessMapPower = ShaderGUI.FindProperty("_ThicknessMapPower", props);
            UseSSS = ShaderGUI.FindProperty("_UseSSS", props); 
            UseSpecular = ShaderGUI.FindProperty("_UseSpecular", props);
            UseUV2Emiss = ShaderGUI.FindProperty("_EmissUv2", props);
            EmissScaleWithLight = ShaderGUI.FindProperty("_ScaleWithLight", props);
            EmissTintToColor = ShaderGUI.FindProperty("_EmissTintToColor", props);
            EmissionPower = ShaderGUI.FindProperty("_EmissionPower", props);


            //advanced options
            colorMask = ShaderGUI.FindProperty("_colormask", props);
            stencil = ShaderGUI.FindProperty("_Stencil", props);
            stencilComp = ShaderGUI.FindProperty("_StencilComp", props);
            stencilOp = ShaderGUI.FindProperty("_StencilOp", props);
            stencilFail = ShaderGUI.FindProperty("_StencilFail", props);
            stencilZFail = ShaderGUI.FindProperty("_StencilZFail", props);
            zwrite = ShaderGUI.FindProperty("_ZWrite", props);
            ztest = ShaderGUI.FindProperty("_ZTest", props);

            RampBaseAnchor = ShaderGUI.FindProperty("_RampBaseAnchor", props);

            //Show Properties in Inspector
            //materialEditor.ShaderProperty(, .displayName);   

            // Swap between blend modes 
            EditorGUI.BeginChangeCheck();
            {

                EditorGUI.BeginChangeCheck();

                EditorGUI.showMixedValue = advMode.hasMixedValue;
                var aMode = (DisplayType)advMode.floatValue;

                EditorGUI.BeginChangeCheck();
                aMode = (DisplayType)EditorGUILayout.Popup("Shader Mode", (int)aMode, Enum.GetNames(typeof(DisplayType)));

                if (EditorGUI.EndChangeCheck())
                {
                    materialEditor.RegisterPropertyChangeUndo("Shader Mode");
                    advMode.floatValue = (float)aMode;
                    EditorGUI.showMixedValue = false;
                }

                materialEditor.ShaderProperty(culling, culling.displayName);
                
                //rimlight
                    materialEditor.ShaderProperty(rimStyle, Styles.rimLightTypeText);

                    if (rimStyle.floatValue == 0)
                    {
                        materialEditor.ShaderProperty(rimWidth, Styles.rimWidthText, 2);
                        materialEditor.ShaderProperty(rimIntensity, Styles.rimIntText, 2);
                        materialEditor.ShaderProperty(rimColor, "Rimlight Tint", 2);
                    }

                    if (rimStyle.floatValue == 1)
                    {
                        materialEditor.ShaderProperty(rimWidth, Styles.rimWidthText, 2);
                        materialEditor.ShaderProperty(rimIntensity, Styles.rimIntText, 2);
                        materialEditor.ShaderProperty(rimColor, "Rimlight Tint", 2);
                    }

                    if (rimStyle.floatValue == 2)
                {
                    material.SetFloat("_RimIntensity", 0);
                }
                //-----

                //main
                    //Rect rect = (0,0);
                    XSStyles.Separator();
                    EditorGUILayout.BeginHorizontal();
                        materialEditor.TexturePropertySingleLine(Styles.MainTexText, mainTex, tint);
                        XSStyles.helpPopup(showHelp, "Main Texture Slots", "The Main Texture - provides color, and Alpha in the case of Transparency. \n\n Occlusion is used to have areas always be shadowed. This is done with a black to white texture.", "Okay");
                    EditorGUILayout.EndHorizontal();
                    GUI.skin = null;
                        materialEditor.ShaderProperty(saturation, Styles.Saturation, 3);

                        materialEditor.TexturePropertySingleLine(Styles.occlusionMap, occlusionMap); 
                        
                        if ( material.GetTexture("_OcclusionMap") )
                        {
                           materialEditor.ShaderProperty(occlusionStrength, "Strength", 3);
                        }
                        else
                        {
                            material.SetFloat("_OcclusionStrength", 1);
                        }
                //cutoff
                    if (material.shader == Shader.Find("Xiexe/Toon/XSToonCutout"))
                    {
                        materialEditor.ShaderProperty(alphaCutoff, Styles.cutoutText);
                    }
                //-----

                //normal map
                    XSStyles.Separator();
                        EditorGUILayout.BeginHorizontal();
                        materialEditor.TexturePropertySingleLine(Styles.normalText, normal, normalStrength);
                    XSStyles.helpPopup(showHelp, "Normal Map", "The normal map - controls how light and shadow distorts on the surface of an object. \n\n Detail Normals can be tiled for small micro details, such as scratches on a metal surface.", "Okay");
                    EditorGUILayout.EndHorizontal();
                        materialEditor.TextureScaleOffsetProperty(normal);
                        materialEditor.TexturePropertySingleLine(Styles.detailNormal, detailNormal, detailNormalStrength);
                        materialEditor.TextureScaleOffsetProperty(detailNormal);
                        materialEditor.TexturePropertySingleLine(Styles.detailMask, detailMask);
                //-----
                
                //shadow ramp
                    XSStyles.Separator();
                    EditorGUILayout.BeginHorizontal();
                        materialEditor.TexturePropertySingleLine(Styles.rampText, shadowRamp);
                        XSStyles.helpPopup(showHelp, "Shadow Ramp", "A gradient texture - horizontal or vertical. Used to control how shadows look. I.E. A smooth gradient would result in smooth shadows. \n\n If your ramp is colored, you can switch to \"Use Ramp Color\" to make your shadows inherit the color of the ramp, otherwise, your shadows will be colored based on the environment. \n\n The Shadow Style ONLY effects shadows cast onto you by other objects.", "Okay");
                    EditorGUILayout.EndHorizontal();
                        materialEditor.ShaderProperty(rampColor, "Ramp Mode", 2);
                        materialEditor.ShaderProperty(shadowType, Styles.shadowTypeText, 2);


                        //ambient
                        //ramp
                        //mixed
                        if (rampColor.floatValue == 0)
                        {
                            material.EnableKeyword("_WORLDSHADOWCOLOR_ON");
                        }
                        if (rampColor.floatValue == 1)
                        {
                            material.DisableKeyword("_WORLDSHADOWCOLOR_ON");
                            material.DisableKeyword("_MIXEDSHADOWCOLOR_ON");
                        }
                        if(rampColor.floatValue == 2)
                        {
                            material.DisableKeyword("_WORLDSHADOWCOLOR_ON");
                            material.EnableKeyword("_MIXEDSHADOWCOLOR_ON");
                        }
                    XSStyles.callGradientEditor();
                //-----  

                //emission
                    XSStyles.Separator();
                        EditorGUILayout.BeginHorizontal();
                        materialEditor.ShaderProperty(emissiveToggle, "Emission");
                        XSStyles.helpPopup(showHelp, "Emission", "Emission is the act of a surface emitting light. \n\nThe Emission Map is generally a black and white texture used to mark where emission can happen. Black would cut off all emission, while white would allow full emission in a given area. \n\n You can adjust how bright the emission is, and the color of it, through the color picker. ", "Okay");
                    EditorGUILayout.EndHorizontal();
                    GUI.skin = null;
                    if (emissiveToggle.floatValue == 0)
                    {
                        materialEditor.TexturePropertySingleLine(Styles.emissText, emissiveTex, emissiveColor);
                        materialEditor.ShaderProperty(EmissTintToColor, "Tint To Diffuse");
                        materialEditor.ShaderProperty(UseUV2Emiss, "UV Channel");
                        materialEditor.ShaderProperty(EmissScaleWithLight, "Scale With Light");
                        if(EmissScaleWithLight.floatValue == 0){
                            materialEditor.ShaderProperty(EmissionPower, "Scale", 2);
                        }
                    }
                    else
                    {
                        material.SetColor("_EmissiveColor", Color.black);
                    }
                //-----

                //specular
                    XSStyles.Separator();
                    EditorGUILayout.BeginHorizontal();
                        materialEditor.ShaderProperty(UseSpecular, "Specular");
                        XSStyles.helpPopup(showHelp, "Specularity", "Specular reflections are the result of light bouncing off of an object. \n\nThis effect is generally used to show gloss on things such as a shiny plastic material. \n\nYou can mask out where reflections can happen with the Specular Map, and you can make the reflections reflect in a pattern with the Specular Pattern. The default texture, for instance, would reflect light in the pattern of lines.", "Okay");
                    EditorGUILayout.EndHorizontal();
                        EditorGUI.BeginChangeCheck();
                        if (UseSpecular.floatValue == 0)
                        {    
                                materialEditor.TexturePropertySingleLine(Styles.specMapText, specMap);
                            GUI.skin = null;
                                materialEditor.TextureScaleOffsetProperty(specMap);
                                materialEditor.TexturePropertySingleLine(Styles.specPatternText, specPattern);
                                materialEditor.TextureScaleOffsetProperty(specPattern);
                                materialEditor.ShaderProperty(stylizedType, "Specular Type");
                                materialEditor.ShaderProperty(specStyle, "Specular Style");
                            if (stylizedType.floatValue == 1)
                            {
                                material.EnableKeyword("_ANISTROPIC_ON");
                                materialEditor.ShaderProperty(aX, "Length", 3);
                                materialEditor.ShaderProperty(aY, "Width", 3);
                            }
                            else
                            {
                                material.DisableKeyword("_ANISTROPIC_ON");
                                materialEditor.ShaderProperty(specArea, Styles.SmoothnessText, 3);
                            }
                            materialEditor.ShaderProperty(specIntensity, Styles.sintensityText, 3);
                        }
                        else
                        {
                            material.SetFloat("_SpecularIntensity", 0);
                        }
                //-----

                //metallic
                    XSStyles.Separator();
                        EditorGUILayout.BeginHorizontal();
                        materialEditor.ShaderProperty(useRefl, "Reflections");
                        XSStyles.helpPopup(showHelp, "Reflections", "This panel is all about reflections. XSToon supports many styles of reflections. \n\n-PBR \n This is what you think about when you think reflections - these will sample the reflection probes in a room and reflect them back off of the surface. \n\n-Matcap \n This takes a SphereMap texture and maps it based on your viewing direction to the surface, to simulate reflections. \n\n -Cubemap \n This is just a cubemap reflection, you can plug any cubemap in and have it reflect, as if you were in that environment.", "Okay");
                    EditorGUILayout.EndHorizontal();
                    GUI.skin = null;
                        if (useRefl.floatValue == 0)
                        {
                            materialEditor.ShaderProperty(reflType, "Reflection Style");
                            material.EnableKeyword("_REFLECTIONS_ON");
                            //pbr
                            if (reflType.floatValue == 0)
                            {
                                materialEditor.TexturePropertySingleLine(Styles.bakedCube, bakedCube);
                                material.EnableKeyword("_PBRREFL_ON");
                                material.DisableKeyword("_MATCAP_ON");
                                material.DisableKeyword("_MATCAP_CUBEMAP_ON");
                                materialEditor.TexturePropertySingleLine(Styles.MetalMap, metalMap);
                                materialEditor.ShaderProperty(metal, "Metallic", 2);
                                materialEditor.TexturePropertySingleLine(Styles.roughMap, roughMap);
                                materialEditor.ShaderProperty(reflSmooth, "Roughness", 2);
                            }
                            //matcap
                            if (reflType.floatValue == 1)
                            {
                                material.EnableKeyword("_MATCAP_ON");
                                material.DisableKeyword("_MATCAP_CUBEMAP_ON");
                                material.DisableKeyword("_PBRREFL_ON");
                                materialEditor.ShaderProperty(matcapStyle, "Blend Mode");
                                materialEditor.TexturePropertySingleLine(Styles.Matcap, metalMap);
                                materialEditor.TexturePropertySingleLine(Styles.MatcapMask, roughMap);
                                materialEditor.ShaderProperty(metal, "Intensity", 2);
                                materialEditor.ShaderProperty(reflSmooth, "Blur", 2);
                            }
                            //bakedcubemap
                            if (reflType.floatValue == 2)
                            {
                                material.EnableKeyword("_MATCAP_CUBEMAP_ON");
                                material.DisableKeyword("_MATCAP_ON");
                                material.DisableKeyword("_PBRREFL_ON");
                                materialEditor.ShaderProperty(matcapStyle, "Blend Mode");
                                materialEditor.TexturePropertySingleLine(Styles.MatcapCubemap, bakedCube);
                                materialEditor.TexturePropertySingleLine(Styles.MatcapMask, roughMap);
                                materialEditor.ShaderProperty(metal, "Intensity", 2);
                                materialEditor.ShaderProperty(reflSmooth, "Roughness", 2);
                            }
                        }
                        else
                        {
                            material.DisableKeyword("_REFLECTIONS_ON");
                            material.DisableKeyword("_PBRREFL_ON");
                            material.DisableKeyword("_MATCAP_ON");
                            material.DisableKeyword("_MATCAP_CUBEMAP_ON");
                        }
                //-----

                //Subsurface Scattering
                XSStyles.Separator();
                    EditorGUILayout.BeginHorizontal();
                        materialEditor.ShaderProperty(UseSSS, "Subsurface Scattering");
                        XSStyles.helpPopup(showHelp, "Subsurface Scattering", "Subsurface scattering is the act of light bouncing and scattering inside of a material before exiting the other side. This can be used to simulate the softness of skin, or things such as marble statues, which might have light bounces around and make them appear 'translucent.'", "Okay");
                        GUI.skin = null;
                    EditorGUILayout.EndHorizontal();
                    if(UseSSS.floatValue == 0)
                    {
                         materialEditor.TexturePropertySingleLine(Styles.thicknessMap, ThicknessMap);
                         materialEditor.ShaderProperty(invertThickness, "Invert", 3);
                         materialEditor.ShaderProperty(ThicknessMapPower, "Power", 3);
                         materialEditor.ShaderProperty(SSSCol, "Subsurface Color", 2);
                         materialEditor.ShaderProperty(SSSDist, "Displacement",2);
                         materialEditor.ShaderProperty(SSSPow, "Sharpness",2);
                         materialEditor.ShaderProperty(SSSIntensity, "Intensity",2);
                    }
                    else{
                        material.SetFloat("_SSSIntensity", 0);
                    }
                //-----



                GUI.skin = null;
                if (advMode.floatValue == 1)
                {
                    XSStyles.Separator();
                    GUILayout.Label(Styles.advancedOptions, EditorStyles.boldLabel);
                    //Stencil
                    GUILayout.Label("Stencil Buffer", EditorStyles.boldLabel);
                    materialEditor.ShaderProperty(colorMask, colorMask.displayName, 2);
                    materialEditor.ShaderProperty(stencil, stencil.displayName, 2);
                    materialEditor.ShaderProperty(stencilComp, stencilComp.displayName, 2);
                    materialEditor.ShaderProperty(stencilOp, stencilOp.displayName, 2);
                    materialEditor.ShaderProperty(stencilFail, stencilFail.displayName, 2);
                    materialEditor.ShaderProperty(stencilZFail, stencilZFail.displayName, 2);
                    materialEditor.ShaderProperty(ztest, ztest.displayName, 2);
                    materialEditor.ShaderProperty(zwrite, zwrite.displayName, 2);
                    materialEditor.ShaderProperty(uv2, "UV2 for Normal/Spec", 2);
                    materialEditor.ShaderProperty(RampBaseAnchor, "Ramp Anchor", 2);
                    
                    // Reset ZWrite/ZTest
                    XSStyles.ResetAdv(material);
                    XSStyles.ResetAdvAll(material);

                }

            }
        }
        DoFooter();
    }
    public static void SetShadowRamp(MaterialProperty shadowRamp, Texture ramp)
    {
        ramp = XSGradientEditor.shadowRamp;
        shadowRamp.textureValue = ramp;
    }
}
