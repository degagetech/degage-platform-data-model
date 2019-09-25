﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace Degage.DataModel.Schema.Toolbox
{
    public partial class ExcelExportConfigControl : BaseExportConfigControl
    {
        public ExcelExportTemplateConfig ExcelExportTemplateConfig { get; private set; }
        public override ExportConfig ExportConfig { get => this._exportConfig; }
        public override ISchemaExporter SchemaExporter { get => this._schemaExporter; }
        public String ExcelExportTemplateDirectory
        {
            get
            {
                return this._schemaExporter.ExcelExportTemplateDirectory;
            }
        }
        public String ExcelExportTemplateConfigPath
        {
            get
            {
                return this._schemaExporter.ExcelExportTemplateConfigPath;
            }
        }

        private ExcelExportConfig _exportConfig;
        private ExcelSchemaExporter _schemaExporter;
        private IList<TableSchemaExtend> _checkedTableSchemas;

        public ExcelExportConfigControl()
        {
            InitializeComponent();

            _exportConfig = new ExcelExportConfig();
            _exportConfig.GroupInfos = new GroupInfoCollection();
            _schemaExporter = new ExcelSchemaExporter();

        }

        private void _checkCustomGroup_CheckedChanged(Object sender, EventArgs e)
        {
            this._pannelGroup.Enabled = this._checkCustomGroup.Checked;
            _exportConfig.IsCustomGroup = this._checkCustomGroup.Checked;
            if (!this._checkCustomGroup.Checked)
            {
                this._lvGroup.Items.Clear();
                _exportConfig.GroupInfos.Clear();
            }
            else
            {
                this._checkedTableSchemas = this.ExportContext.CheckedTableSchemas;
            }

        }

        private void _btnEditGroup_Click(Object sender, EventArgs e)
        {
            if (this._checkedTableSchemas == null || this._checkedTableSchemas.Count <= 0) return;
            ExcelExportGroupForm form = new ExcelExportGroupForm();
            form.SetGroupInfo(this._exportConfig.GroupInfos, this._checkedTableSchemas);
            form.ShowDialog(this);
            this.RenderingGroupInfo();
        }
        public override Boolean IsValidExportConfig(out String errorMessage)
        {
            if (this._cbExcelExportTemplate.SelectedValue == null)
            {
                errorMessage = "样式模板";
                return false;
            }
            errorMessage = null;
            return true;
        }
        public override void ImportConfigInfo(String configString)
        {
            this._exportConfig = JsonSerializer.Deserialize<ExcelExportConfig>(configString);
            this._exportConfig.GroupInfos.ReIndex();
            this.RenderingConfigInfo();

        }
        private void RenderingConfigInfo()
        {
            this._checkMerge.Checked = this._exportConfig.IsMergeGroupToSheet;
            this._checkCustomGroup.Checked = this._exportConfig.IsCustomGroup;
            this._cbExcelExportTemplate.SelectedText = this._exportConfig.ExcelTempldateName;
            this._checkExclude.Checked = this._exportConfig.EnableExclude;
            this.RenderingGroupInfo();
        }
        public override String ExportConfigInfo()
        {
            return JsonSerializer.Serialize(this._exportConfig);
        }
        public async override void Initialize(IExportContext context)
        {
            base.Initialize(context);
            this._cbExcelExportTemplate.DisplayMember = nameof(ExcelExportTemplateItem.Name);
            this._cbExcelExportTemplate.ValueMember = nameof(ExcelExportTemplateItem.Path);
            context.CheckedTableSchemaChanged += Form_CheckedTableSchemaChanged;
            this.ExcelExportTemplateConfig = new ExcelExportTemplateConfig();
            await this.ExcelExportTemplateConfig.LoadAsync(this.ExcelExportTemplateConfigPath);
            this._cbExcelExportTemplate.DataSource = this.ExcelExportTemplateConfig.TemplateItems;

        }
        private void Reanalyse(IList<TableSchemaExtend> schemas)
        {
            if (this._checkedTableSchemas != null && this._lvGroup.Items.Count > 0)
            {
                HashSet<String> newItem = new HashSet<String>(schemas.Select(t => t.Name));
                foreach (var schema in this._checkedTableSchemas)
                {
                    if (!newItem.Contains(schema.Name))
                    {
                        this._lvGroup.Items.RemoveByKey(schema.Name);
                        this._exportConfig.GroupInfos.RemoveItem(schema.Name);
                    }
                }
            }
            this._checkedTableSchemas = schemas;
        }
        private void Form_CheckedTableSchemaChanged(Object sender, IList<TableSchemaExtend> schemas)
        {
            this.Reanalyse(schemas);
        }
        private void RenderingGroupInfo()
        {
            this._lvGroup.Items.Clear();
            this._lvGroup.Groups.Clear();
            if (this._exportConfig.GroupInfos.Count > 0)
            {

                this._lvGroup.BeginUpdate();
                this._lvGroup.SuspendLayout();
                foreach (var pair in this._exportConfig.GroupInfos)
                {
                    ListViewGroup group = new ListViewGroup(pair.Key);
                    this._lvGroup.Groups.Add(group);
                    IList<String> schemaNames = pair.Value;
                    if (schemaNames != null && schemaNames.Count > 0)
                    {
                        this._checkExclude.Enabled = true;
                        foreach (String name in schemaNames)
                        {
                            ListViewItem item = new ListViewItem(name);
                            item.Name = name;
                            item.Group = group;
                            item.ImageIndex = 0;
                            this._lvGroup.Items.Add(item);
                        }
                    }

                }
                this._lvGroup.EndUpdate();
                this._lvGroup.ResumeLayout();
            }
            else
            {
                this._checkExclude.Checked = false;
                this._checkExclude.Enabled = false;
            }

        }

        private async void _btnStyleTemplate_Click(object sender, EventArgs e)
        {
            var items = this.ExcelExportTemplateConfig.TemplateItems.ToList();
            //生成数据的副本，防止在用户取消操作后影响原来的数据
            var jsonStr = JsonSerializer.Serialize(items);
            var newItems = JsonSerializer.Deserialize<List<ExcelExportTemplateItem>>(jsonStr);
            ExcelExportStyleTemplateForm form = new ExcelExportStyleTemplateForm(newItems,this._schemaExporter);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.ExcelExportTemplateConfig.TemplateItems = newItems.ToArray();
                this._cbExcelExportTemplate.DataSource = this.ExcelExportTemplateConfig.TemplateItems;
                await this.ExcelExportTemplateConfig.SaveAsync(this.ExcelExportTemplateConfigPath);
            }

        }

        private void _checkMerge_CheckedChanged(object sender, EventArgs e)
        {
            this._exportConfig.IsMergeGroupToSheet = this._checkMerge.Checked;
        }

        private void _cbExcelExportTemplate_SelectedIndexChanged(Object sender, EventArgs e)
        {
            this._exportConfig.ExcelTemplatePath = this._cbExcelExportTemplate.SelectedValue.ToString();
            this._exportConfig.ExcelTempldateName = this._cbExcelExportTemplate.Text;
        }

        private void _checkExclude_CheckedChanged(object sender, EventArgs e)
        {
            this._exportConfig.EnableExclude = this._checkMerge.Checked;
        }
    }

}
