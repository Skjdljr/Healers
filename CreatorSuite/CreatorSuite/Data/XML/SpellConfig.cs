﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


// 
// This source code was auto-generated by xsd, Version=4.0.30319.18020.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class SpellConfig {
    
    private SpellConfigSpell[] spellField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Spell")]
    public SpellConfigSpell[] Spell {
        get {
            return this.spellField;
        }
        set {
            this.spellField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class SpellConfigSpell {
    
    private int resourceCostField;
    
    private decimal powerField;
    
    private decimal coolDownField;
    
    private decimal castTimeField;

    private string effectField;

    private string spellTypeField;
    
    private string spellSchoolField;
    
    private string classTypeField;
    
    private string imagePathField;
    
    private string nameField;

    private int idField;

    private int radiusField;

    private decimal durationField;

    private int intervalField;

    /// <remarks/>
    public int resourceCost {
        get {
            return this.resourceCostField;
        }
        set {
            this.resourceCostField = value;
        }
    }
    
    /// <remarks/>
    public decimal power {
        get {
            return this.powerField;
        }
        set {
            this.powerField = value;
        }
    }
    
    /// <remarks/>
    public decimal coolDown {
        get {
            return this.coolDownField;
        }
        set {
            this.coolDownField = value;
        }
    }
    
    /// <remarks/>
    public decimal castTime {
        get {
            return this.castTimeField;
        }
        set {
            this.castTimeField = value;
        }
    }

    /// <remarks/>
    public string effectType
    {
        get
        {
            return this.effectField;
        }
        set
        {
            this.effectField = value;
        }
    }

    /// <remarks/>
    public string spellType {
        get {
            return this.spellTypeField;
        }
        set {
            this.spellTypeField = value;
        }
    }
    
    /// <remarks/>
    public string spellSchool {
        get {
            return this.spellSchoolField;
        }
        set {
            this.spellSchoolField = value;
        }
    }
    
    /// <remarks/>
    public string classType {
        get {
            return this.classTypeField;
        }
        set {
            this.classTypeField = value;
        }
    }
    
    /// <remarks/>
    public string imagePath {
        get {
            return this.imagePathField;
        }
        set {
            this.imagePathField = value;
        }
    }

       /// <remarks/>
    public int radius
    {
        get
        {
            return this.radiusField;
        }
        set
        {
            this.radiusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal duration
    {
        get
        {
            return this.durationField;
        }
        set
        {
            this.durationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int interval
    {
        get
        {
            return this.intervalField;
        }
        set
        {
            this.intervalField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int ID {
        get {
            return this.idField;
        }
        set {
            this.idField = value;
        }
    }
}
