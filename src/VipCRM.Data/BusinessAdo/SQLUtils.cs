using System;
using System.Text;

namespace VipCRM.Data.BusinessAdo
{
    public class SQLUtils
    {
        // Fields
        private AdjustmentMethod _adjustment;
        private StringBuilder _builder;
        private DataBaseType _dbtype;
        private string _id;
        private int _posTag1;
        private int _posTag2;
        private string _tag1;
        private string _tag2;
        private string FINAL_TAG_FORMAT;
        private const string ID_FORMAT = " id={0}";
        private string INITIAL_TAG_FORMAT;
        private string[] TAG_ADJUSTMENTS;
        private string[] TAG_KEYWORDS;
        private string[] TAG_RADICALS;
        private string[] TAG_SEPARATORS;

        // Methods
        public SQLUtils()
        {
            this.TAG_SEPARATORS = new string[] { "xxx", " and ", "," };
            this.TAG_RADICALS = new string[] { "select", "from", "where", "groupby", "having", "orderby" };
            this.TAG_KEYWORDS = new string[] { "select", "from", "where", "group by", "having", "order by" };
            this.TAG_ADJUSTMENTS = new string[] { "", " i", " f", " auto" };
            this._builder = new StringBuilder();
        }

        public SQLUtils(StringBuilder builder)
        {
            this.TAG_SEPARATORS = new string[] { "xxx", " and ", "," };
            this.TAG_RADICALS = new string[] { "select", "from", "where", "groupby", "having", "orderby" };
            this.TAG_KEYWORDS = new string[] { "select", "from", "where", "group by", "having", "order by" };
            this.TAG_ADJUSTMENTS = new string[] { "", " i", " f", " auto" };
            this._builder = builder;
        }

        public SQLUtils(string sql, DataBaseType dbtype)
        {
            this.TAG_SEPARATORS = new string[] { "xxx", " and ", "," };
            this.TAG_RADICALS = new string[] { "select", "from", "where", "groupby", "having", "orderby" };
            this.TAG_KEYWORDS = new string[] { "select", "from", "where", "group by", "having", "order by" };
            this.TAG_ADJUSTMENTS = new string[] { "", " i", " f", " auto" };
            this._builder = new StringBuilder(sql);
            this._dbtype = dbtype;
            if (dbtype == DataBaseType.MySQL)
            {
                this.INITIAL_TAG_FORMAT = "-- {0}{1}{2} --";
                this.FINAL_TAG_FORMAT = "-- /{0}{1} --";
            }
            else
            {
                this.INITIAL_TAG_FORMAT = "--<{0}{1}{2}>--";
                this.FINAL_TAG_FORMAT = "--</{0}{1}>--";
            }
        }

        public static StringBuilder addAND(StringBuilder stringBuilder, string texto)
        {
            StringBuilder builder = stringBuilder;
            if ((builder.ToString().Trim().Length > 0) && (texto.Trim().Length > 0))
            {
                builder.Append(" AND " + texto);
                return builder;
            }
            builder.Append(texto);
            return builder;
        }

        public static StringBuilder addOR(StringBuilder stringBuilder, string texto)
        {
            StringBuilder builder = stringBuilder;
            if ((builder.ToString().Trim().Length > 0) && (texto.Trim().Length > 0))
            {
                builder.Append(" OR " + texto);
                return builder;
            }
            builder.Append(texto);
            return builder;
        }

        public static StringBuilder appendLine(StringBuilder stringBuilder, string texto)
        {
            StringBuilder builder = stringBuilder;
            return builder.Append(texto + Environment.NewLine);
        }

        private TagStatus checkStatusTag()
        {
            if ((this._posTag1 < 0) && (this._posTag2 < 0))
            {
                return TagStatus.Missing;
            }
            if (this._posTag2 < 0)
            {
                return TagStatus.Single;
            }
            if ((this._posTag2 >= (this._posTag1 + this._tag1.Length)) && (this._posTag1 >= 0))
            {
                return TagStatus.Decimal;
            }
            return TagStatus.Wrong;
        }

        public void handleSQLAppendFrom(string from)
        {
            this.handleSQLAppendFrom(from, string.Empty);
        }

        public void handleSQLAppendFrom(string from, string id)
        {
            this.modifySQL(TagOperation.Append, TagType.From, from, id);
        }

        public void handleSQLAppendGroupBy(string groupBy)
        {
            this.handleSQLAppendGroupBy(groupBy, string.Empty);
        }

        public void handleSQLAppendGroupBy(string groupBy, string id)
        {
            this.modifySQL(TagOperation.Append, TagType.GroupBy, groupBy, id);
        }

        public void handleSQLAppendHaving(string having)
        {
            this.handleSQLAppendHaving(having, string.Empty);
        }

        public void handleSQLAppendHaving(string having, string id)
        {
            this.modifySQL(TagOperation.Append, TagType.Having, having, id);
        }

        public void handleSQLAppendOrderBy(string orderBy)
        {
            this.handleSQLAppendOrderBy(orderBy, string.Empty);
        }

        public void handleSQLAppendOrderBy(string orderBy, string id)
        {
            this.modifySQL(TagOperation.Append, TagType.OrderBy, orderBy, id);
        }

        public void handleSQLAppendSelect(string select)
        {
            this.handleSQLAppendSelect(select, string.Empty);
        }

        public void handleSQLAppendSelect(string select, string id)
        {
            this.modifySQL(TagOperation.Append, TagType.Select, select, id);
        }

        public void handleSQLAppendWhere(string where)
        {
            this.handleSQLAppendWhere(where, string.Empty);
        }

        public void handleSQLAppendWhere(string where, string id)
        {
            this.modifySQL(TagOperation.Append, TagType.Where, where, id);
        }

        public void handleSQLDrop()
        {
            this.handleSQLDrop(string.Empty);
        }

        public void handleSQLDrop(string id)
        {
            foreach (TagType type in Enum.GetValues(typeof(TagType)))
            {
                this.modifySQL(TagOperation.Drop, type, string.Empty, id);
            }
        }

        public void handleSQLFrom(string from)
        {
            this.handleSQLFrom(from, string.Empty);
        }

        public void handleSQLFrom(string from, string id)
        {
            this.modifySQL(TagOperation.Replace, TagType.From, from, id);
        }

        public void handleSQLGroupBy(string groupBy)
        {
            this.handleSQLGroupBy(groupBy, string.Empty);
        }

        public void handleSQLGroupBy(string groupBy, string id)
        {
            this.modifySQL(TagOperation.Replace, TagType.GroupBy, groupBy, id);
        }

        public void handleSQLHaving(string having)
        {
            this.handleSQLHaving(having, string.Empty);
        }

        public void handleSQLHaving(string having, string id)
        {
            this.modifySQL(TagOperation.Replace, TagType.Having, having, id);
        }

        public void handleSQLOrderBy(string orderBy)
        {
            this.handleSQLOrderBy(orderBy, string.Empty);
        }

        public void handleSQLOrderBy(string orderBy, string id)
        {
            this.modifySQL(TagOperation.Replace, TagType.OrderBy, orderBy, id);
        }

        public void handleSQLSelect(string select)
        {
            this.handleSQLSelect(select, string.Empty);
        }

        public void handleSQLSelect(string select, string id)
        {
            this.modifySQL(TagOperation.Replace, TagType.Select, select, id);
        }

        public void handleSQLWhere(string where)
        {
            this.handleSQLWhere(where, string.Empty);
        }

        public void handleSQLWhere(string where, string id)
        {
            this.modifySQL(TagOperation.Replace, TagType.Where, where, id);
        }

        private void identifyTags(TagType tagType)
        {
            string str = this._builder.ToString().ToLower();
            this._posTag1 = 0;
            this._posTag2 = 0;
            foreach (AdjustmentMethod method in Enum.GetValues(typeof(AdjustmentMethod)))
            {
                this._tag1 = string.Format(this.INITIAL_TAG_FORMAT, this.TAG_RADICALS[(int)tagType], this._id, this.TAG_ADJUSTMENTS[(int)method]);
                this._posTag1 = str.IndexOf(this._tag1);
                if (this._posTag1 >= 0)
                {
                    this._adjustment = method;
                    break;
                }
            }
            this._tag2 = string.Format(this.FINAL_TAG_FORMAT + Environment.NewLine, this.TAG_RADICALS[(int)tagType], this._id);
            this._posTag2 = str.IndexOf(this._tag2);
        }

        private void modifySQL(TagOperation operation, TagType tagType, string modification)
        {
            this.modifySQL(operation, tagType, modification, string.Empty);
        }

        private void modifySQL(TagOperation operation, TagType tagType, string modification, string id)
        {
            ElementSeparator aND;
            string str = string.Empty;
            if (id.Trim().Length > 0)
            {
                this._id = string.Format(" id={0}", id).ToLower();
            }
            else
            {
                this._id = string.Empty;
            }
            this.identifyTags(tagType);
            TagStatus status = this.checkStatusTag();
            switch (status)
            {
                case TagStatus.Missing:
                    if (modification.Trim().Length > 0)
                    {
                        throw new ESQLUtils(string.Format("Tag n\x00e3o encontrada:{0}{1}", Environment.NewLine, this._tag1));
                    }
                    break;

                case TagStatus.Wrong:
                    throw new ESQLUtils(string.Format("Erro encontrado em rela\x00e7\x00e3o a tag [{0}{1}]", Environment.NewLine, this.TAG_RADICALS[(int)tagType]));
            }
            if ((tagType == TagType.Where) || (tagType == TagType.Having))
            {
                aND = ElementSeparator.AND;
            }
            else
            {
                aND = ElementSeparator.Comma;
            }
            if ((operation == TagOperation.Append) && (status == TagStatus.Single))
            {
                operation = TagOperation.Replace;
            }
            if (operation == TagOperation.Append)
            {
                if (modification.Trim().Length == 0)
                {
                    return;
                }
                if (this._builder.ToString(this._posTag1 + this._tag1.Length, (this._posTag2 - this._posTag1) - this._tag1.Length).Trim().Length > 0)
                {
                    if (this._adjustment == AdjustmentMethod.End)
                    {
                        str = modification + this.TAG_SEPARATORS[(int)aND];
                    }
                    else
                    {
                        str = this.TAG_SEPARATORS[(int)aND] + modification;
                    }
                }
                else
                {
                    str = modification;
                }
                this._builder.Insert(this._posTag2, str + Environment.NewLine);
            }
            if (operation == TagOperation.Replace)
            {
                if (status == TagStatus.Decimal)
                {
                    this._builder.Remove(this._posTag1 + this._tag1.Length, ((this._posTag2 + this._tag2.Length) - this._posTag1) - this._tag1.Length);
                }
                if (modification.Trim().Length == 0)
                {
                    return;
                }
                switch (this._adjustment)
                {
                    case AdjustmentMethod.None:
                        str = modification;
                        break;

                    case AdjustmentMethod.Beginning:
                        str = this.TAG_SEPARATORS[(int)aND] + modification;
                        break;

                    case AdjustmentMethod.End:
                        str = modification + this.TAG_SEPARATORS[(int)aND];
                        break;

                    case AdjustmentMethod.Auto:
                        str = this.TAG_KEYWORDS[(int)tagType] + Environment.NewLine + modification;
                        break;
                }
                this._builder.Insert(this._posTag1 + this._tag1.Length, Environment.NewLine + str + Environment.NewLine + this._tag2);
            }
            if (operation == TagOperation.Drop)
            {
                switch (status)
                {
                    case TagStatus.Decimal:
                        this._builder.Remove(this._posTag2, this._tag2.Length);
                        break;

                    case TagStatus.Single:
                        this._builder.Remove(this._posTag1, this._tag1.Length);
                        break;
                }
            }
        }

        public override string ToString()
        {
            return this._builder.ToString();
        }

        // Properties
        public StringBuilder builder
        {
            get
            {
                return this._builder;
            }
        }
    }

    internal class ESQLUtils : SystemException
    {
        // Methods
        public ESQLUtils(string msg)
            : base(msg)
        {
        }
    }
}