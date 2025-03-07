using Real_Estate_Agency;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Real_Estate_Agency {
    public partial class Form1 : Form {

        const string DBName = "database.real_estate_agency";

        PropertiesTableHandler propH;
        PeopleTableHander peopleH;
        CustomersTableHandler customersH;
        public Form1() {
            InitializeComponent();
            peopleH = new PeopleTableHander(peopleGridView);
            propH = new PropertiesTableHandler(propertyGridView, peopleH);
            customersH = new CustomersTableHandler(customersDataGridView, peopleH, propH);

            propH.UpdateComboBoxes();
            LoadFromDisk();


        }

        private void LoadFromDisk() {



            peopleH.Validate();
            propH.Validate();
            customersH.Validate();

            if(peopleH.CheckForErrors() || propH.CheckForErrors() || customersH.CheckForErrors()) {
                MessageBox.Show(
                        "Ячейки содержат ошибки!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                return;
            }


            if (File.Exists(DBName))
                using (var stream = File.OpenRead(DBName)) {
                    peopleH.Load(stream);
                    propH.Load(stream);
                    customersH.Load(stream);
                }
        }

        void AddMissplacedIDs() {
            peopleH.AddMissplacedIDs();
            propH.AddMissplacedIDs();
            customersH.AddMissplacedIDs();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e) {
            SaveToDisk();
        }

        private void SaveToDisk() {

            peopleH.Validate();
            propH.Validate();
            customersH.Validate();
            
            if (peopleH.CheckForErrors() || propH.CheckForErrors() || customersH.CheckForErrors()) {
                MessageBox.Show(
                        "Ячейки содержат ошибки!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                return;
            }

            var a = peopleH.ApplyChanges();
            var b = propH.ApplyChanges();
            var c = customersH.ApplyChanges();

            AddMissplacedIDs();

            using (var stream = File.Create(DBName)) {
                peopleH.Save(stream);
                propH.Save(stream);
                customersH.Save(stream);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
            AddMissplacedIDs();
            switch (tabControl1.SelectedIndex) {
                case 0: propH.UpdateComboBoxes(); break;
                case 1: peopleH.UpdateComboBoxes(); break;
                case 2: customersH.UpdateComboBoxes(); break;
            }
        }

        private void loadToolStripButton_Click(object sender, EventArgs e) {
            LoadFromDisk();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            SaveToDisk();
        }
    }

    public interface ISaveable {
        void Save(Stream output);
        void Load(Stream input);
    }


    // Строимся на предположении, что ID - это первая колонка.
    abstract class TableHandler<T> : IEnumerable<T> where T : IIDIdentifyable, ISaveable {

        protected readonly DataGridView Grid;

        public virtual void Validate() { }

        public bool ApplyChanges() => Grid.EndEdit();
        public void RevertChanges() => Grid.CancelEdit();

        public bool CheckForErrors() {
                bool hasErrorText = false;
                foreach (DataGridViewRow row in this.Grid.Rows) {
                    foreach (DataGridViewCell cell in row.Cells) {
                        if (cell.ErrorText.Length > 0) {
                            hasErrorText = true;
                            break;
                        }
                    }
                    if (hasErrorText)
                        break;
                }
                return hasErrorText;
            }

            public TableHandler(DataGridView grid) {
            Grid = grid;

        }

        public void AddMissplacedIDs() {
            int maxId = -1;

            // Выполним поиск в два прохода
            for (int row = 0; row < Grid.NewRowIndex; row++) {
                var cell = Grid.Rows[row].Cells[0];
                var value = cell.Value;

                if (value is int rowId)
                    maxId = rowId > maxId ? rowId : maxId;
            }

            for (int row = 0; row < Grid.NewRowIndex; row++) {
                var cell = Grid.Rows[row].Cells[0];
                var value = cell.Value;

                if (!(value is int))
                    cell.Value = ++maxId;
            }
        }

        protected void UpdateList<E>(string updatedColumn, TableHandler<E> source) where E : IIDIdentifyable, ISaveable {
            var values = source.Where(x => x.ID.HasValue).Select(x => new Pair<E> {
                Value = x,
                Name = x.DisplayName
            }).ToArray();

            var column = Grid.Columns[updatedColumn] as DataGridViewComboBoxColumn;
            column.DisplayMember = "Name";
            column.ValueMember = "Value";
            column.DataSource = values;

            for (int rowIndex = 0; rowIndex < Grid.NewRowIndex; rowIndex++) {
                var row = Grid.Rows[rowIndex];
                var cell = row.Cells[column.Index];
                cell.Value =
                    values.
                    Select(x => x.Value).
                    Where(x => IDIdentifyable.IDEquals(x, cell.Value as IIDIdentifyable)).
                    FirstOrDefault();
            }
        }

        public int EntryCount => Grid.NewRowIndex;

        public virtual void UpdateComboBoxes() {

        }

        public void Save(Stream output) {
            using (BinaryWriter writer = new BinaryWriter(output, Encoding.UTF8, true)) {
                writer.Write(EntryCount);

                foreach (var entry in this) {
                    entry.Save(output);
                }
            }
        }

        public void Load(Stream input) {
            using (BinaryReader reader = new BinaryReader(input, Encoding.UTF8, true)) {
                int count = reader.ReadInt32();
                Clear();
                for (int x = 0; x < count; x++) {
                    Grid.Rows.AddCopy(Grid.NewRowIndex);
                }

                foreach (var entry in this) {
                    entry.Load(input);
                }

                UpdateComboBoxes();
            }
        }

        private IEnumerable<T> UpdateEnumaerable() => Enumerable.
                Range(0, Grid.NewRowIndex).
                Select(x => CreateBasedOnRow(Grid.Rows[x]));

        public T FindByID(int id) =>
            UpdateEnumaerable().Where(i => i.ID == id).FirstOrDefault();

        protected abstract T CreateBasedOnRow(DataGridViewRow row);

        public void Clear() {
            Grid.Rows.Clear();
        }

        public IEnumerator<T> GetEnumerator() => UpdateEnumaerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public interface IHuman : IIDIdentifyable, ISaveable {
        int? ID { get; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string Patronymic { get; set; }
        string Pass { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
    }


    static class Utils {

        public static string ExtractRawNumber(string formated) {
            formated = formated.Replace(" ", "").
                Replace("-", "").
                Replace("(", "").
                Replace(")", "");

            if (formated.Length > 1 && formated[0] == '+') {
                if (int.TryParse(formated[1].ToString(), out var result))
                    formated = (result + 1) + formated.Substring(2);
            }

            return formated;
        }

        public static string FormatNumber(string numberRaw) {
            numberRaw = ExtractRawNumber(numberRaw);

            if (numberRaw.Length > 0) {

                if (numberRaw.Length > 3) {
                    if (numberRaw.Length > 9) {
                        numberRaw = numberRaw.Substring(0, 7) + "-" + numberRaw.Substring(7, 2) + "-" + numberRaw.Substring(9);
                    }

                    numberRaw = numberRaw[0] + " (" + numberRaw.Substring(1, 3) + ") " + numberRaw.Substring(4);
                }

                if (numberRaw[0] == '8') {
                    numberRaw = "+7" + numberRaw.Substring(1);
                }
            }


            return numberRaw;
        }

        public static string ExtractRawPass(string formated) {
            return formated.Replace(" ", "");
        }

        public static string FormatPass(string raw) {
            raw = ExtractRawPass(raw);
            if (raw.Length > 1) {
                if (raw.Length > 3) {
                    raw = raw.Substring(0, 4) + " " + raw.Substring(4);
                }

                raw = raw.Substring(0, 2) + " " + raw.Substring(2);
            }

            return raw;
        }
    }

    public enum PropertyKind {
        MovableProperty = 0,
        RealEstate = 1
    }

    static class IDIdentifyable {
        public static bool IDEquals(IIDIdentifyable a, IIDIdentifyable b) {
            if (a is null || b is null)
                return false;

            return a.ID == b.ID;
        }
    }

    public interface IIDIdentifyable {
        int? ID { get; }
        string DisplayName { get; }
    }


    public interface IProperty : IIDIdentifyable, ISaveable {
        int? ID { get; }

        string Name { get; set; }

        decimal Cost { get; set; }

        IHuman Owner { get; set; }

        PropertyKind Kind { get; set; }
    }

    public interface ICustomer : IIDIdentifyable, ISaveable {
        int? ID { get; }
        IHuman People { get; set; }
        IProperty Property { get; set; }
    }



    class PeopleTableHander : TableHandler<IHuman> {
        private class HumanImpl : IHuman {
            private readonly DataGridViewRow m_row;

            public HumanImpl(DataGridViewRow row) {
                m_row = row;
            }

            public int? ID => m_row.Cells[0].Value as int?;

            public string LastName {
                get => m_row.Cells["PeopleLastName"].Value as string;
                set => m_row.Cells["PeopleLastName"].Value = value;
            }
            public string FirstName {
                get => m_row.Cells["PeopleFirstName"].Value as string;
                set => m_row.Cells["PeopleFirstName"].Value = value;
            }
            public string Patronymic {
                get => m_row.Cells["PeoplePatronymic"].Value as string;
                set => m_row.Cells["PeoplePatronymic"].Value = value;
            }
            public string Pass {
                get => m_row.Cells["PeoplePass"].Value as string;
                set => m_row.Cells["PeoplePass"].Value = value;
            }
            public string Phone {
                get => m_row.Cells["PeoplePhone"].Value as string;
                set => m_row.Cells["PeoplePhone"].Value = value;
            }
            public string Email {
                get => m_row.Cells["PeopleEmail"].Value as string;
                set => m_row.Cells["PeopleEmail"].Value = value;
            }

            public string DisplayName {
                get {
                    var name = "";

                    if (!string.IsNullOrWhiteSpace(LastName))
                        name = LastName.Trim();

                    if (!string.IsNullOrWhiteSpace(FirstName))
                        name += " " + FirstName.Trim();

                    if (!string.IsNullOrWhiteSpace(Patronymic))
                        name += " " + Patronymic.Trim();

                    if (!string.IsNullOrWhiteSpace($"{Pass}{Phone}{Email}")) {
                        name += " (";

                        string innerName = "";

                        if (!string.IsNullOrWhiteSpace(Pass))
                            innerName = ", " + Utils.FormatPass(Pass);

                        if (!string.IsNullOrWhiteSpace(Phone))
                            innerName += ", " + Utils.FormatNumber(Phone);

                        if (!string.IsNullOrWhiteSpace(Email))
                            innerName += ", " + Email.Trim();

                        if (innerName.Length >= 2)
                            name += innerName.Substring(2);

                        name += ")";
                    }

                    return name.Trim();
                }
            }

            public void Load(Stream input) {
                using (BinaryReader reader = new BinaryReader(input, Encoding.UTF8, true)) {
                    m_row.Cells[0].Value = reader.ReadInt32();

                    LastName = reader.ReadString();
                    FirstName = reader.ReadString();
                    Patronymic = reader.ReadString();

                    Pass = Utils.FormatPass(reader.ReadString());
                    Phone = Utils.FormatNumber(reader.ReadString());
                    Email = reader.ReadString();
                }
            }

            public void Save(Stream output) {
                using (BinaryWriter writer = new BinaryWriter(output, Encoding.UTF8, true)) {

                    writer.Write(ID.Value);
                    writer.Write(LastName?.Trim() ?? "");
                    writer.Write(FirstName?.Trim() ?? "");
                    writer.Write(Patronymic?.Trim() ?? "");

                    writer.Write(Utils.ExtractRawPass(Pass ?? ""));
                    writer.Write(Utils.ExtractRawNumber(Phone ?? ""));
                    writer.Write(Email?.Trim() ?? "");
                }
            }
        }

        public PeopleTableHander(DataGridView grid) : base(grid) {
        }

        protected override IHuman CreateBasedOnRow(DataGridViewRow row)
            => new HumanImpl(row);
    }

    public class Pair<T> {
        public T Value { get; set; }
        public string Name { get; set; }
    }

    class PropertiesTableHandler : TableHandler<IProperty> {
        private readonly PeopleTableHander m_peopleHandler;

        public PropertiesTableHandler(DataGridView grid, PeopleTableHander peopleTableHandler) : base(grid) {
            m_peopleHandler = peopleTableHandler;

            var kindColumn = Grid.Columns["PropertyKind"] as DataGridViewComboBoxColumn;
            kindColumn.ValueMember = "Value";
            kindColumn.DisplayMember = "Name";
            kindColumn.DataSource = new Pair<PropertyKind>[] {
                new Pair < PropertyKind >(){
                    Value = PropertyKind.MovableProperty,
                    Name = "Движимое имущество"
                },
                new Pair < PropertyKind >(){
                    Value = PropertyKind.RealEstate,
                    Name = "Недвижимое имущество"
                }
            };

            grid.CellValidating += (sender, args) => {
                var col = Grid.Columns["PropertyCost"];


                if (col.Index == args.ColumnIndex) {
                    var cell = Grid.Rows[args.RowIndex].Cells[args.ColumnIndex];
                    var str = args.FormattedValue?.ToString();

                    args.Cancel = !decimal.TryParse(
                        str,
                        NumberStyles.Integer | NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture,
                        out _
                        ) && !string.IsNullOrWhiteSpace(str);


                    cell.ErrorText = args.Cancel ? "Должно быть число. Может содержать точку в качестве разделителя" : null;
                }

            };

            grid.RowValidating += (sender, args) => {
                var col = Grid.Columns["PropertyCost"];


                if (col.Index == args.ColumnIndex) {
                    var cell = Grid.Rows[args.RowIndex].Cells[args.ColumnIndex];
                    var str = cell.FormattedValue?.ToString();

                    args.Cancel = !decimal.TryParse(
                        str,
                        NumberStyles.Integer | NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture,
                        out _
                        ) && !string.IsNullOrWhiteSpace(str);


                    cell.ErrorText = args.Cancel ? "Должно быть число. Может содержать точку в качестве разделителя" : null;
                }

            };

            grid.Validating += (sender, args) => {
                var col = Grid.Columns["PropertyCost"];
                for (int x = 0; x < EntryCount; x++) {
                    var cell = Grid.Rows[x].Cells[col.Index];

                    var str = cell.FormattedValue?.ToString();

                    args.Cancel = !decimal.TryParse(
                        str,
                        NumberStyles.Integer | NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture,
                        out _
                        ) && !string.IsNullOrWhiteSpace(str);


                    cell.ErrorText = args.Cancel ? "Должно быть число. Может содержать точку в качестве разделителя" : null;
                    if (args.Cancel)
                        return;
                }
            };
        }

        // TODO: DRY!!!!!   
        public override void Validate() {
            var col = Grid.Columns["PropertyCost"];
            for (int x = 0; x < EntryCount; x++) {
                var cell = Grid.Rows[x].Cells[col.Index];

                var str = cell.FormattedValue?.ToString();

                bool cancel = !decimal.TryParse(
                    str,
                    NumberStyles.Integer | NumberStyles.AllowDecimalPoint,
                    CultureInfo.InvariantCulture,
                    out _
                    ) && !string.IsNullOrWhiteSpace(str);


                cell.ErrorText = cancel ? "Должно быть число. Может содержать точку в качестве разделителя" : null;
                if (cancel)
                    return;
            }
        }

        class PropertyImpl : IProperty {
            private readonly DataGridViewRow m_row;
            private readonly PropertiesTableHandler m_handler;

            public PropertyImpl(DataGridViewRow row, PropertiesTableHandler owner) {
                m_row = row;
                m_handler = owner;
            }

            public int? ID => m_row.Cells[0].Value as int?;

            public string Name {
                get => m_row.Cells["PropertyName"].Value as string;
                set => m_row.Cells["PropertyName"].Value = value;
            }
            public decimal Cost {
                get => decimal.TryParse(
                    m_row.Cells["PropertyCost"].Value?.ToString() ?? "",
                    NumberStyles.Integer | NumberStyles.AllowDecimalPoint,
                    CultureInfo.InvariantCulture, out var d)
                    ? d : 0;

                set => m_row.Cells["PropertyCost"].Value = value.ToString(CultureInfo.InvariantCulture);
            }
            public IHuman Owner {
                get => m_row.Cells["PropertyOwnerID"].Value as IHuman;
                set => m_row.Cells["PropertyOwnerID"].Value = value;
            }
            public PropertyKind Kind {
                get => m_row.Cells["PropertyKind"].Value as PropertyKind? ?? PropertyKind.RealEstate;
                set => m_row.Cells["PropertyKind"].Value = value;
            }

            public string DisplayName {
                get {
                    string name = Name;

                    if (Owner is object)
                        name += $" (Владеет: {Owner.DisplayName})";

                    return name;
                }
            }

            public void Load(Stream input) {
                using (BinaryReader reader = new BinaryReader(input, Encoding.UTF8, true)) {
                    m_row.Cells[0].Value = reader.ReadInt32();
                    Name = reader.ReadString();
                    Cost = reader.ReadDecimal();
                    Kind = (PropertyKind)reader.ReadByte();
                    Owner = m_handler.m_peopleHandler.FindByID(reader.ReadInt32());
                }
            }

            public void Save(Stream output) {
                using (BinaryWriter writer = new BinaryWriter(output, Encoding.UTF8, true)) {
                    writer.Write(ID.Value);
                    writer.Write(Name?.Trim() ?? "");
                    writer.Write(Cost);
                    writer.Write((byte)Kind);
                    writer.Write(Owner?.ID ?? -1);
                }
            }
        }




        public override void UpdateComboBoxes() {

            UpdateList("PropertyOwnerID", m_peopleHandler);
        }

        protected override IProperty CreateBasedOnRow(DataGridViewRow row)
            => new PropertyImpl(row, this);
    }

    class CustomersTableHandler : TableHandler<ICustomer> {
        private readonly PeopleTableHander m_peopleHandler;
        private readonly PropertiesTableHandler m_propertiesHandler;

        public CustomersTableHandler(DataGridView grid, PeopleTableHander p, PropertiesTableHandler c) : base(grid) {
            m_peopleHandler = p;
            m_propertiesHandler = c;
        }

        class CustomerImpl : ICustomer {
            private readonly DataGridViewRow m_row;
            private readonly CustomersTableHandler m_handler;

            public CustomerImpl(DataGridViewRow row, CustomersTableHandler owner) {
                m_row = row;
                m_handler = owner;
            }

            public int? ID => m_row.Cells["CustomersID"].Value as int?;

            public IHuman People {
                get => m_row.Cells["CustomersHumanID"].Value as IHuman;
                set => m_row.Cells["CustomersHumanID"].Value = value;
            }
            public IProperty Property {
                get => m_row.Cells["CustomersPropertyID"].Value as IProperty;
                set => m_row.Cells["CustomersPropertyID"].Value = value;

            }

            public string DisplayName => $"{People.DisplayName} хочет {Property.DisplayName}";

            public void Load(Stream input) {
                using (BinaryReader reader = new BinaryReader(input, Encoding.UTF8, true)) {

                    m_row.Cells[0].Value = reader.ReadInt32();
                    People = m_handler.m_peopleHandler.FindByID(reader.ReadInt32());
                    Property = m_handler.m_propertiesHandler.FindByID(reader.ReadInt32());
                }
            }

            public void Save(Stream output) {
                using (BinaryWriter writer = new BinaryWriter(output, Encoding.UTF8, true)) {
                    writer.Write(ID.Value);
                    writer.Write(People?.ID ?? -1);
                    writer.Write(Property?.ID ?? -1);
                }
            }
        }

        public override void UpdateComboBoxes() {

            UpdateList("CustomersHumanID", m_peopleHandler);
            UpdateList("CustomersPropertyID", m_propertiesHandler);
        }

        protected override ICustomer CreateBasedOnRow(DataGridViewRow row)
            => new CustomerImpl(row, this);
    }
}