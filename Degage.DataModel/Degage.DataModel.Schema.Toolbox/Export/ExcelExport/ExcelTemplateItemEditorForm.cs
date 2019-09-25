﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Degage.DataModel.Schema.Toolbox
{
    public partial class ExcelTemplateItemEditorForm : BaseForm
    {
        public ExcelExportTemplateConfig ExportTemplateConfig { get; private set; }
        public ExcelTemplateItemEditorForm()
        {
            InitializeComponent();
        }
        public ExcelTemplateItemEditorForm(ExcelExportTemplateConfig config) : this()
        {
            this.ExportTemplateConfig = config;
            this._pgTemplateItemEditor.SelectedObject = config;
        }
    }
}
