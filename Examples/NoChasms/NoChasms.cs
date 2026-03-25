using System.Collections.Generic;

public class NoChasms {
    private float m_originalAirValue;
    private float m_originalEarthValue;
    private float m_originalLifeValue;
    private float m_originalFireValue;
    private float m_originalShadowValue;
    private float m_originalWaterValue;
    
    public void OnEnable(Dictionary<string, object> dependencies) {
        m_originalAirValue = Element.Air.GetData().m_terrainData.m_waterThreshold;
        m_originalEarthValue = Element.Earth.GetData().m_terrainData.m_waterThreshold;
        m_originalLifeValue = Element.Life.GetData().m_terrainData.m_waterThreshold;
        m_originalFireValue = Element.Fire.GetData().m_terrainData.m_waterThreshold;
        m_originalShadowValue = Element.Shadow.GetData().m_terrainData.m_waterThreshold;
        m_originalWaterValue = Element.Water.GetData().m_terrainData.m_waterThreshold;
        
        Element.Air.GetData().m_terrainData.m_waterThreshold = 0;
        Element.Earth.GetData().m_terrainData.m_waterThreshold = 0;
        Element.Life.GetData().m_terrainData.m_waterThreshold = 0;
        Element.Fire.GetData().m_terrainData.m_waterThreshold = 0;
        Element.Shadow.GetData().m_terrainData.m_waterThreshold = 0;
        Element.Water.GetData().m_terrainData.m_waterThreshold = 0;
    }

    public void OnDisable() {
        Element.Air.GetData().m_terrainData.m_waterThreshold = m_originalAirValue;
        Element.Earth.GetData().m_terrainData.m_waterThreshold = m_originalEarthValue;
        Element.Life.GetData().m_terrainData.m_waterThreshold = m_originalLifeValue;
        Element.Fire.GetData().m_terrainData.m_waterThreshold = m_originalFireValue;
        Element.Shadow.GetData().m_terrainData.m_waterThreshold = m_originalShadowValue;
        Element.Water.GetData().m_terrainData.m_waterThreshold = m_originalWaterValue;
    }
}