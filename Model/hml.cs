namespace DAR.Model
{
[System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class hml
        {

            private hmlType[] typesField;

            private hmlAttr[] attributesField;

            private hmlProperty[] propertiesField;

            private hmlHist[] tphField;

            private hmlDep[] ardField;

            private hmlStates statesField;

            private hmlTable[] xttField;

            private object callbacksField;

            private decimal versionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("type", IsNullable = false)]
            public hmlType[] types
            {
                get
                {
                    return this.typesField;
                }
                set
                {
                    this.typesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("attr", IsNullable = false)]
            public hmlAttr[] attributes
            {
                get
                {
                    return this.attributesField;
                }
                set
                {
                    this.attributesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("property", IsNullable = false)]
            public hmlProperty[] properties
            {
                get
                {
                    return this.propertiesField;
                }
                set
                {
                    this.propertiesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("hist", IsNullable = false)]
            public hmlHist[] tph
            {
                get
                {
                    return this.tphField;
                }
                set
                {
                    this.tphField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("dep", IsNullable = false)]
            public hmlDep[] ard
            {
                get
                {
                    return this.ardField;
                }
                set
                {
                    this.ardField = value;
                }
            }

            /// <remarks/>
            public hmlStates states
            {
                get
                {
                    return this.statesField;
                }
                set
                {
                    this.statesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("table", IsNullable = false)]
            public hmlTable[] xtt
            {
                get
                {
                    return this.xttField;
                }
                set
                {
                    this.xttField = value;
                }
            }

            /// <remarks/>
            public object callbacks
            {
                get
                {
                    return this.callbacksField;
                }
                set
                {
                    this.callbacksField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlType
        {

            private string descField;

            private hmlTypeDomain domainField;

            private string idField;

            private string nameField;

            private string baseField;

            private byte lengthField;

            private bool lengthFieldSpecified;

            private byte scaleField;

            private bool scaleFieldSpecified;

            /// <remarks/>
            public string desc
            {
                get
                {
                    return this.descField;
                }
                set
                {
                    this.descField = value;
                }
            }

            /// <remarks/>
            public hmlTypeDomain domain
            {
                get
                {
                    return this.domainField;
                }
                set
                {
                    this.domainField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @base
            {
                get
                {
                    return this.baseField;
                }
                set
                {
                    this.baseField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte length
            {
                get
                {
                    return this.lengthField;
                }
                set
                {
                    this.lengthField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool lengthSpecified
            {
                get
                {
                    return this.lengthFieldSpecified;
                }
                set
                {
                    this.lengthFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte scale
            {
                get
                {
                    return this.scaleField;
                }
                set
                {
                    this.scaleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool scaleSpecified
            {
                get
                {
                    return this.scaleFieldSpecified;
                }
                set
                {
                    this.scaleFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTypeDomain
        {

            private hmlTypeDomainValue[] valueField;

            private string orderedField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("value")]
            public hmlTypeDomainValue[] value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ordered
            {
                get
                {
                    return this.orderedField;
                }
                set
                {
                    this.orderedField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTypeDomainValue
        {

            private string isField;

            private decimal fromField;

            private bool fromFieldSpecified;

            private decimal toField;

            private bool toFieldSpecified;

            private byte numField;

            private bool numFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @is
            {
                get
                {
                    return this.isField;
                }
                set
                {
                    this.isField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal from
            {
                get
                {
                    return this.fromField;
                }
                set
                {
                    this.fromField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool fromSpecified
            {
                get
                {
                    return this.fromFieldSpecified;
                }
                set
                {
                    this.fromFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal to
            {
                get
                {
                    return this.toField;
                }
                set
                {
                    this.toField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool toSpecified
            {
                get
                {
                    return this.toFieldSpecified;
                }
                set
                {
                    this.toFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte num
            {
                get
                {
                    return this.numField;
                }
                set
                {
                    this.numField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool numSpecified
            {
                get
                {
                    return this.numFieldSpecified;
                }
                set
                {
                    this.numFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlAttr
        {

            private string descField;

            private string idField;

            private string typeField;

            private string nameField;

            private string clbField;

            private string abbrevField;

            private string classField;

            private string commField;

            /// <remarks/>
            public string desc
            {
                get
                {
                    return this.descField;
                }
                set
                {
                    this.descField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string clb
            {
                get
                {
                    return this.clbField;
                }
                set
                {
                    this.clbField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string abbrev
            {
                get
                {
                    return this.abbrevField;
                }
                set
                {
                    this.abbrevField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @class
            {
                get
                {
                    return this.classField;
                }
                set
                {
                    this.classField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string comm
            {
                get
                {
                    return this.commField;
                }
                set
                {
                    this.commField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlProperty
        {

            private hmlPropertyAttref[] attrefField;

            private string idField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("attref")]
            public hmlPropertyAttref[] attref
            {
                get
                {
                    return this.attrefField;
                }
                set
                {
                    this.attrefField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlPropertyAttref
        {

            private string refField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @ref
            {
                get
                {
                    return this.refField;
                }
                set
                {
                    this.refField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlHist
        {

            private string srcField;

            private string dstField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string src
            {
                get
                {
                    return this.srcField;
                }
                set
                {
                    this.srcField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string dst
            {
                get
                {
                    return this.dstField;
                }
                set
                {
                    this.dstField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlDep
        {

            private string independentField;

            private string dependentField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string independent
            {
                get
                {
                    return this.independentField;
                }
                set
                {
                    this.independentField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string dependent
            {
                get
                {
                    return this.dependentField;
                }
                set
                {
                    this.dependentField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlStates
        {

            private hmlStatesState stateField;

            /// <remarks/>
            public hmlStatesState state
            {
                get
                {
                    return this.stateField;
                }
                set
                {
                    this.stateField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlStatesState
        {

            private object[] itemsField;

            private string idField;

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("attref", typeof(hmlStatesStateAttref))]
            [System.Xml.Serialization.XmlElementAttribute("set", typeof(hmlStatesStateSet))]
            public object[] Items
            {
                get
                {
                    return this.itemsField;
                }
                set
                {
                    this.itemsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlStatesStateAttref
        {

            private string refField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @ref
            {
                get
                {
                    return this.refField;
                }
                set
                {
                    this.refField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlStatesStateSet
        {

            private hmlStatesStateSetValue valueField;

            /// <remarks/>
            public hmlStatesStateSetValue value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlStatesStateSetValue
        {

            private ushort isField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort @is
            {
                get
                {
                    return this.isField;
                }
                set
                {
                    this.isField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTable
        {

            private hmlTableSchm schmField;

            private hmlTableRule[] ruleField;

            private string idField;

            private string nameField;

            /// <remarks/>
            public hmlTableSchm schm
            {
                get
                {
                    return this.schmField;
                }
                set
                {
                    this.schmField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("rule")]
            public hmlTableRule[] rule
            {
                get
                {
                    return this.ruleField;
                }
                set
                {
                    this.ruleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableSchm
        {

            private hmlTableSchmAttref[] preconditionField;

            private hmlTableSchmConclusion conclusionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("attref", IsNullable = false)]
            public hmlTableSchmAttref[] precondition
            {
                get
                {
                    return this.preconditionField;
                }
                set
                {
                    this.preconditionField = value;
                }
            }

            /// <remarks/>
            public hmlTableSchmConclusion conclusion
            {
                get
                {
                    return this.conclusionField;
                }
                set
                {
                    this.conclusionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableSchmAttref
        {

            private string refField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @ref
            {
                get
                {
                    return this.refField;
                }
                set
                {
                    this.refField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableSchmConclusion
        {

            private hmlTableSchmConclusionAttref attrefField;

            /// <remarks/>
            public hmlTableSchmConclusionAttref attref
            {
                get
                {
                    return this.attrefField;
                }
                set
                {
                    this.attrefField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableSchmConclusionAttref
        {

            private string refField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @ref
            {
                get
                {
                    return this.refField;
                }
                set
                {
                    this.refField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRule
        {

            private hmlTableRuleRelation[] conditionField;

            private hmlTableRuleDecision decisionField;

            private hmlTableRuleLink linkField;

            private string idField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("relation", IsNullable = false)]
            public hmlTableRuleRelation[] condition
            {
                get
                {
                    return this.conditionField;
                }
                set
                {
                    this.conditionField = value;
                }
            }

            /// <remarks/>
            public hmlTableRuleDecision decision
            {
                get
                {
                    return this.decisionField;
                }
                set
                {
                    this.decisionField = value;
                }
            }

            /// <remarks/>
            public hmlTableRuleLink link
            {
                get
                {
                    return this.linkField;
                }
                set
                {
                    this.linkField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleRelation
        {

            private hmlTableRuleRelationAttref attrefField;

            private hmlTableRuleRelationValue[] setField;

            private string nameField;

            /// <remarks/>
            public hmlTableRuleRelationAttref attref
            {
                get
                {
                    return this.attrefField;
                }
                set
                {
                    this.attrefField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("value", IsNullable = false)]
            public hmlTableRuleRelationValue[] set
            {
                get
                {
                    return this.setField;
                }
                set
                {
                    this.setField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleRelationAttref
        {

            private string refField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @ref
            {
                get
                {
                    return this.refField;
                }
                set
                {
                    this.refField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleRelationValue
        {

            private string isField;

            private short fromField;

            private bool fromFieldSpecified;

            private ushort toField;

            private bool toFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @is
            {
                get
                {
                    return this.isField;
                }
                set
                {
                    this.isField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public short from
            {
                get
                {
                    return this.fromField;
                }
                set
                {
                    this.fromField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool fromSpecified
            {
                get
                {
                    return this.fromFieldSpecified;
                }
                set
                {
                    this.fromFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort to
            {
                get
                {
                    return this.toField;
                }
                set
                {
                    this.toField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool toSpecified
            {
                get
                {
                    return this.toFieldSpecified;
                }
                set
                {
                    this.toFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecision
        {

            private hmlTableRuleDecisionTrans transField;

            /// <remarks/>
            public hmlTableRuleDecisionTrans trans
            {
                get
                {
                    return this.transField;
                }
                set
                {
                    this.transField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecisionTrans
        {

            private hmlTableRuleDecisionTransAttref attrefField;

            private hmlTableRuleDecisionTransSet setField;

            private hmlTableRuleDecisionTransExpr exprField;

            /// <remarks/>
            public hmlTableRuleDecisionTransAttref attref
            {
                get
                {
                    return this.attrefField;
                }
                set
                {
                    this.attrefField = value;
                }
            }

            /// <remarks/>
            public hmlTableRuleDecisionTransSet set
            {
                get
                {
                    return this.setField;
                }
                set
                {
                    this.setField = value;
                }
            }

            /// <remarks/>
            public hmlTableRuleDecisionTransExpr expr
            {
                get
                {
                    return this.exprField;
                }
                set
                {
                    this.exprField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecisionTransAttref
        {

            private string refField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @ref
            {
                get
                {
                    return this.refField;
                }
                set
                {
                    this.refField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecisionTransSet
        {

            private hmlTableRuleDecisionTransSetValue valueField;

            /// <remarks/>
            public hmlTableRuleDecisionTransSetValue value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecisionTransSetValue
        {

            private decimal isField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal @is
            {
                get
                {
                    return this.isField;
                }
                set
                {
                    this.isField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecisionTransExpr
        {

            private hmlTableRuleDecisionTransExprAttref attrefField;

            private hmlTableRuleDecisionTransExprValue valueField;

            private hmlTableRuleDecisionTransExprExpr exprField;

            private string nameField;

            /// <remarks/>
            public hmlTableRuleDecisionTransExprAttref attref
            {
                get
                {
                    return this.attrefField;
                }
                set
                {
                    this.attrefField = value;
                }
            }

            /// <remarks/>
            public hmlTableRuleDecisionTransExprValue value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }

            /// <remarks/>
            public hmlTableRuleDecisionTransExprExpr expr
            {
                get
                {
                    return this.exprField;
                }
                set
                {
                    this.exprField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecisionTransExprAttref
        {

            private string refField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @ref
            {
                get
                {
                    return this.refField;
                }
                set
                {
                    this.refField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecisionTransExprValue
        {

            private byte isField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte @is
            {
                get
                {
                    return this.isField;
                }
                set
                {
                    this.isField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecisionTransExprExpr
        {

            private hmlTableRuleDecisionTransExprExprExpr exprField;

            private hmlTableRuleDecisionTransExprExprValue valueField;

            private string nameField;

            /// <remarks/>
            public hmlTableRuleDecisionTransExprExprExpr expr
            {
                get
                {
                    return this.exprField;
                }
                set
                {
                    this.exprField = value;
                }
            }

            /// <remarks/>
            public hmlTableRuleDecisionTransExprExprValue value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecisionTransExprExprExpr
        {

            private hmlTableRuleDecisionTransExprExprExprAttref attrefField;

            private hmlTableRuleDecisionTransExprExprExprExpr exprField;

            private string nameField;

            /// <remarks/>
            public hmlTableRuleDecisionTransExprExprExprAttref attref
            {
                get
                {
                    return this.attrefField;
                }
                set
                {
                    this.attrefField = value;
                }
            }

            /// <remarks/>
            public hmlTableRuleDecisionTransExprExprExprExpr expr
            {
                get
                {
                    return this.exprField;
                }
                set
                {
                    this.exprField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecisionTransExprExprExprAttref
        {

            private string refField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @ref
            {
                get
                {
                    return this.refField;
                }
                set
                {
                    this.refField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecisionTransExprExprExprExpr
        {

            private hmlTableRuleDecisionTransExprExprExprExprAttref[] attrefField;

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("attref")]
            public hmlTableRuleDecisionTransExprExprExprExprAttref[] attref
            {
                get
                {
                    return this.attrefField;
                }
                set
                {
                    this.attrefField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecisionTransExprExprExprExprAttref
        {

            private string refField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @ref
            {
                get
                {
                    return this.refField;
                }
                set
                {
                    this.refField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleDecisionTransExprExprValue
        {

            private decimal isField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal @is
            {
                get
                {
                    return this.isField;
                }
                set
                {
                    this.isField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleLink
        {

            private hmlTableRuleLinkTabref tabrefField;

            /// <remarks/>
            public hmlTableRuleLinkTabref tabref
            {
                get
                {
                    return this.tabrefField;
                }
                set
                {
                    this.tabrefField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class hmlTableRuleLinkTabref
        {

            private string refField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @ref
            {
                get
                {
                    return this.refField;
                }
                set
                {
                    this.refField = value;
                }
            }
        }
}