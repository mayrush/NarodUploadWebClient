namespace NarodUploadWebClient
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsFill = new System.Windows.Forms.ToolStripButton();
            this.tsSplitButtonView = new System.Windows.Forms.ToolStripSplitButton();
            this.tsViewDetailed = new System.Windows.Forms.ToolStripMenuItem();
            this.tsViewLarge = new System.Windows.Forms.ToolStripMenuItem();
            this.tsViewSmall = new System.Windows.Forms.ToolStripMenuItem();
            this.tsViewList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsViewTile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsView = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsFolder = new System.Windows.Forms.ToolStripButton();
            this.tsUpload = new System.Windows.Forms.ToolStripButton();
            this.tsSettings = new System.Windows.Forms.ToolStripButton();
            this.tsSend = new System.Windows.Forms.ToolStripButton();
            this.tsChangeViewType = new System.Windows.Forms.ToolStripButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.cFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cDays = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cStat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.сUplDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMenuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemProlongate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemChangeFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemSendLink = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrLVScroll = new System.Windows.Forms.Timer(this.components);
            this.tsAboutButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowItemReorder = true;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsFill,
            this.tsSplitButtonView,
            this.tsView,
            this.tsDelete,
            this.toolStripButton1,
            this.tsFolder,
            this.tsUpload,
            this.tsSettings,
            this.tsSend,
            this.tsChangeViewType,
            this.tsAboutButton});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(670, 31);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsFill
            // 
            this.tsFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsFill.Image = global::NarodUploadWebClient.Properties.Resources.reload_24;
            this.tsFill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsFill.Name = "tsFill";
            this.tsFill.Size = new System.Drawing.Size(28, 28);
            this.tsFill.Text = "Обновить";
            this.tsFill.Click += new System.EventHandler(this.BFillClick);
            // 
            // tsSplitButtonView
            // 
            this.tsSplitButtonView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSplitButtonView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsViewDetailed,
            this.tsViewLarge,
            this.tsViewSmall,
            this.tsViewList,
            this.tsViewTile});
            this.tsSplitButtonView.Image = global::NarodUploadWebClient.Properties.Resources.search_24;
            this.tsSplitButtonView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSplitButtonView.Name = "tsSplitButtonView";
            this.tsSplitButtonView.Size = new System.Drawing.Size(40, 28);
            this.tsSplitButtonView.Text = "Вид";
            this.tsSplitButtonView.ButtonClick += new System.EventHandler(this.BViewClick);
            // 
            // tsViewDetailed
            // 
            this.tsViewDetailed.CheckOnClick = true;
            this.tsViewDetailed.Name = "tsViewDetailed";
            this.tsViewDetailed.Size = new System.Drawing.Size(177, 22);
            this.tsViewDetailed.Text = "Таблица";
            this.tsViewDetailed.Click += new System.EventHandler(this.BViewClickDetails);
            // 
            // tsViewLarge
            // 
            this.tsViewLarge.CheckOnClick = true;
            this.tsViewLarge.Name = "tsViewLarge";
            this.tsViewLarge.Size = new System.Drawing.Size(177, 22);
            this.tsViewLarge.Text = "Крупные значки";
            this.tsViewLarge.Click += new System.EventHandler(this.BViewClickLarge);
            // 
            // tsViewSmall
            // 
            this.tsViewSmall.CheckOnClick = true;
            this.tsViewSmall.Name = "tsViewSmall";
            this.tsViewSmall.Size = new System.Drawing.Size(177, 22);
            this.tsViewSmall.Text = "Маленькие значки";
            this.tsViewSmall.Click += new System.EventHandler(this.BViewClickSmall);
            // 
            // tsViewList
            // 
            this.tsViewList.CheckOnClick = true;
            this.tsViewList.Name = "tsViewList";
            this.tsViewList.Size = new System.Drawing.Size(177, 22);
            this.tsViewList.Text = "Список";
            this.tsViewList.Click += new System.EventHandler(this.BViewClickList);
            // 
            // tsViewTile
            // 
            this.tsViewTile.CheckOnClick = true;
            this.tsViewTile.Name = "tsViewTile";
            this.tsViewTile.Size = new System.Drawing.Size(177, 22);
            this.tsViewTile.Text = "Плитка";
            this.tsViewTile.Click += new System.EventHandler(this.BViewClickTile);
            // 
            // tsView
            // 
            this.tsView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsView.Image = global::NarodUploadWebClient.Properties.Resources.search_24;
            this.tsView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsView.Name = "tsView";
            this.tsView.Size = new System.Drawing.Size(28, 28);
            this.tsView.Text = "Вид";
            this.tsView.Visible = false;
            this.tsView.Click += new System.EventHandler(this.BViewClick);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = global::NarodUploadWebClient.Properties.Resources.delete_24;
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(28, 28);
            this.tsDelete.Text = "Удалить";
            this.tsDelete.Click += new System.EventHandler(this.BDeleteClick);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::NarodUploadWebClient.Properties.Resources.date_24;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton1.Text = "Продлить";
            this.toolStripButton1.Click += new System.EventHandler(this.BProlongateClick);
            // 
            // tsFolder
            // 
            this.tsFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsFolder.Image = global::NarodUploadWebClient.Properties.Resources.chfolder_24;
            this.tsFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsFolder.Name = "tsFolder";
            this.tsFolder.Size = new System.Drawing.Size(28, 28);
            this.tsFolder.Text = "Задать папку";
            this.tsFolder.Click += new System.EventHandler(this.TsFolderClick);
            // 
            // tsUpload
            // 
            this.tsUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsUpload.Image = global::NarodUploadWebClient.Properties.Resources.add_24;
            this.tsUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsUpload.Name = "tsUpload";
            this.tsUpload.Size = new System.Drawing.Size(28, 28);
            this.tsUpload.Text = "Загрузить файлы";
            this.tsUpload.Click += new System.EventHandler(this.TsUploadClick);
            // 
            // tsSettings
            // 
            this.tsSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSettings.Image = global::NarodUploadWebClient.Properties.Resources.setting_24;
            this.tsSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSettings.Name = "tsSettings";
            this.tsSettings.Size = new System.Drawing.Size(28, 28);
            this.tsSettings.Text = "Настройки";
            this.tsSettings.Click += new System.EventHandler(this.TsSettingsClick);
            // 
            // tsSend
            // 
            this.tsSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSend.Image = global::NarodUploadWebClient.Properties.Resources.send_user_24;
            this.tsSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSend.Name = "tsSend";
            this.tsSend.Size = new System.Drawing.Size(28, 28);
            this.tsSend.Text = "Отправить ссылки";
            this.tsSend.Click += new System.EventHandler(this.TsSendClick);
            // 
            // tsChangeViewType
            // 
            this.tsChangeViewType.CheckOnClick = true;
            this.tsChangeViewType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsChangeViewType.Image = global::NarodUploadWebClient.Properties.Resources.view_24;
            this.tsChangeViewType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsChangeViewType.Name = "tsChangeViewType";
            this.tsChangeViewType.Size = new System.Drawing.Size(28, 28);
            this.tsChangeViewType.Text = "Не отображать папки";
            this.tsChangeViewType.Click += new System.EventHandler(this.tsChangeViewType_Click);
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.AllowDrop = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cFile,
            this.cID,
            this.cDays,
            this.cSize,
            this.cStat,
            this.сUplDate});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 31);
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(670, 477);
            this.listView1.TabIndex = 13;
            this.listView1.TileSize = new System.Drawing.Size(250, 64);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView1ColumnClick);
            this.listView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView1_ItemDrag);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1SelectedIndexChanged);
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            this.listView1.DragOver += new System.Windows.Forms.DragEventHandler(this.listView1_DragOver);
            this.listView1.DoubleClick += new System.EventHandler(this.ListView1DoubleClick);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            // 
            // cFile
            // 
            this.cFile.Text = "Файл";
            this.cFile.Width = 250;
            // 
            // cID
            // 
            this.cID.DisplayIndex = 2;
            this.cID.Text = "ID";
            this.cID.Width = 75;
            // 
            // cDays
            // 
            this.cDays.DisplayIndex = 4;
            this.cDays.Text = "Дни";
            this.cDays.Width = 50;
            // 
            // cSize
            // 
            this.cSize.DisplayIndex = 1;
            this.cSize.Text = "Размер";
            this.cSize.Width = 75;
            // 
            // cStat
            // 
            this.cStat.DisplayIndex = 3;
            this.cStat.Text = "Скачивания";
            this.cStat.Width = 50;
            // 
            // сUplDate
            // 
            this.сUplDate.Text = "Дата загрузки";
            this.сUplDate.Width = 75;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItemRefresh,
            this.tsMenuItemView,
            this.tsMenuItemDelete,
            this.tsMenuItemProlongate,
            this.tsMenuItemChangeFolder,
            this.tsMenuItemAdd,
            this.tsMenuItemSendLink,
            this.tsMenuItemSettings});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 180);
            // 
            // tsMenuItemRefresh
            // 
            this.tsMenuItemRefresh.Image = global::NarodUploadWebClient.Properties.Resources.reload_24;
            this.tsMenuItemRefresh.Name = "tsMenuItemRefresh";
            this.tsMenuItemRefresh.Size = new System.Drawing.Size(175, 22);
            this.tsMenuItemRefresh.Text = "Обновить";
            this.tsMenuItemRefresh.Click += new System.EventHandler(this.BFillClick);
            // 
            // tsMenuItemView
            // 
            this.tsMenuItemView.Image = global::NarodUploadWebClient.Properties.Resources.search_24;
            this.tsMenuItemView.Name = "tsMenuItemView";
            this.tsMenuItemView.Size = new System.Drawing.Size(175, 22);
            this.tsMenuItemView.Text = "Сменить вид";
            this.tsMenuItemView.Click += new System.EventHandler(this.BViewClick);
            // 
            // tsMenuItemDelete
            // 
            this.tsMenuItemDelete.Image = global::NarodUploadWebClient.Properties.Resources.delete_24;
            this.tsMenuItemDelete.Name = "tsMenuItemDelete";
            this.tsMenuItemDelete.Size = new System.Drawing.Size(175, 22);
            this.tsMenuItemDelete.Text = "Удалить";
            this.tsMenuItemDelete.Click += new System.EventHandler(this.BDeleteClick);
            // 
            // tsMenuItemProlongate
            // 
            this.tsMenuItemProlongate.Image = global::NarodUploadWebClient.Properties.Resources.date_24;
            this.tsMenuItemProlongate.Name = "tsMenuItemProlongate";
            this.tsMenuItemProlongate.Size = new System.Drawing.Size(175, 22);
            this.tsMenuItemProlongate.Text = "Продлить";
            this.tsMenuItemProlongate.Click += new System.EventHandler(this.BProlongateClick);
            // 
            // tsMenuItemChangeFolder
            // 
            this.tsMenuItemChangeFolder.Image = global::NarodUploadWebClient.Properties.Resources.chfolder_24;
            this.tsMenuItemChangeFolder.Name = "tsMenuItemChangeFolder";
            this.tsMenuItemChangeFolder.Size = new System.Drawing.Size(175, 22);
            this.tsMenuItemChangeFolder.Text = "Сменить папку";
            this.tsMenuItemChangeFolder.Click += new System.EventHandler(this.TsFolderClick);
            // 
            // tsMenuItemAdd
            // 
            this.tsMenuItemAdd.Image = global::NarodUploadWebClient.Properties.Resources.add_24;
            this.tsMenuItemAdd.Name = "tsMenuItemAdd";
            this.tsMenuItemAdd.Size = new System.Drawing.Size(175, 22);
            this.tsMenuItemAdd.Text = "Добавить";
            this.tsMenuItemAdd.Click += new System.EventHandler(this.TsUploadClick);
            // 
            // tsMenuItemSendLink
            // 
            this.tsMenuItemSendLink.Image = global::NarodUploadWebClient.Properties.Resources.send_user_24;
            this.tsMenuItemSendLink.Name = "tsMenuItemSendLink";
            this.tsMenuItemSendLink.Size = new System.Drawing.Size(175, 22);
            this.tsMenuItemSendLink.Text = "Отправить ссылку";
            this.tsMenuItemSendLink.Click += new System.EventHandler(this.TsSendClick);
            // 
            // tsMenuItemSettings
            // 
            this.tsMenuItemSettings.Image = global::NarodUploadWebClient.Properties.Resources.setting_24;
            this.tsMenuItemSettings.Name = "tsMenuItemSettings";
            this.tsMenuItemSettings.Size = new System.Drawing.Size(175, 22);
            this.tsMenuItemSettings.Text = "Настройка";
            this.tsMenuItemSettings.Click += new System.EventHandler(this.TsSettingsClick);
            // 
            // tmrLVScroll
            // 
            this.tmrLVScroll.Tick += new System.EventHandler(this.tmrLVScroll_Tick);
            // 
            // tsAboutButton
            // 
            this.tsAboutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsAboutButton.Image = global::NarodUploadWebClient.Properties.Resources.info_24;
            this.tsAboutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAboutButton.Name = "tsAboutButton";
            this.tsAboutButton.Size = new System.Drawing.Size(28, 28);
            this.tsAboutButton.Text = "О программе";
            this.tsAboutButton.Click += new System.EventHandler(this.tsAboutButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(670, 508);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Загрузка файлов на Яндекс.Диск";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormFormClosed);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion Код, автоматически созданный конструктором форм Windows

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsFill;
        private System.Windows.Forms.ToolStripButton tsView;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsFolder;
        private System.Windows.Forms.ToolStripButton tsUpload;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripButton tsSettings;
        private System.Windows.Forms.ToolStripButton tsSend;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemView;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemDelete;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemProlongate;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemChangeFolder;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemSendLink;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemSettings;
        private System.Windows.Forms.ColumnHeader cFile;
        private System.Windows.Forms.ColumnHeader cID;
        private System.Windows.Forms.ColumnHeader cDays;
        private System.Windows.Forms.ToolStripSplitButton tsSplitButtonView;
        private System.Windows.Forms.ToolStripMenuItem tsViewDetailed;
        private System.Windows.Forms.ToolStripMenuItem tsViewLarge;
        private System.Windows.Forms.ToolStripMenuItem tsViewSmall;
        private System.Windows.Forms.ToolStripMenuItem tsViewList;
        private System.Windows.Forms.ToolStripMenuItem tsViewTile;
        private System.Windows.Forms.Timer tmrLVScroll;
        private System.Windows.Forms.ToolStripButton tsChangeViewType;
        private System.Windows.Forms.ColumnHeader cSize;
        private System.Windows.Forms.ColumnHeader cStat;
        private System.Windows.Forms.ColumnHeader сUplDate;
        private System.Windows.Forms.ToolStripButton tsAboutButton;
    }
}