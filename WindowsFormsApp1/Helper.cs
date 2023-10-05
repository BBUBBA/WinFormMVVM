using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>
    /// ReactiveProperty による値、状態のバインドを行うヘルパー。
    /// </summary>
    public static class PropertyBindHelper
    {
        static Tuple<object, string> ResolveLambda<V>(Expression<Func<V>> expression)
        {
            var lambda = expression as LambdaExpression;
            if (lambda == null) throw new ArgumentException();
            var property = lambda.Body as MemberExpression;
            if (property == null) throw new ArgumentException();
            var parent = property.Expression;
            return new Tuple<object, string>(Expression.Lambda(parent).Compile().DynamicInvoke(), property.Member.Name);
        }

        /// <summary>
        /// 値、状態をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="item1">バインドするプロパティの Expression。</param>
        /// <param name="item2">バインドする値のExpression。</param>
        public static void Bind<T, U>(Expression<Func<T>> item1, Expression<Func<U>> item2)
        {
            var tuple1 = ResolveLambda(item1);
            var tuple2 = ResolveLambda(item2);
            var control = tuple1.Item1 as Control;
            if (control == null) throw new ArgumentException();

            if (control != null)
            {
                // 既に設定済みのバインドを解除する
                var binding = control.DataBindings[tuple1.Item2];
                if (binding != null)
                {
                    control.DataBindings.Remove(binding);
                }

                // NOTE: UIのプロパティがNullableの場合、正しくデータバインドできないため、formattingEnabled を true とする。
                control.DataBindings.Add(new Binding(tuple1.Item2, tuple2.Item1, tuple2.Item2, true, DataSourceUpdateMode.OnPropertyChanged));
            }
        }

        /// <summary>
        /// ラベルの値バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="label">ラベルオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void Bind<T>(this Label label, Expression<Func<T>> expression)
        {
            Bind(() => label.Text, expression);
        }

        /// <summary>
        /// テキストボックスの値バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textBox">テキストボックスオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void Bind<T>(this TextBox textBox, Expression<Func<T>> expression)
        {
            Bind(() => textBox.Text, expression);
        }

        public static void Bind(this TextBox control, BindingSource bs, string field)
        {
            var binding = control.DataBindings[field];
            if (binding != null) control.DataBindings.Remove(binding);

            control.DataBindings.Add(new Binding("Text", bs, field, true, DataSourceUpdateMode.OnPropertyChanged));
        }

        /// <summary>
        /// 日付ピッカーの値バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dateTimePicker">日付ピッカーオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void Bind<T>(this DateTimePicker dateTimePicker, Expression<Func<T>> expression)
        {
            Bind(() => dateTimePicker.Value, expression);
        }

        /// <summary>
        /// ラジオボタンの値バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="radioButton">ラジオボタンオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void Bind<T>(this RadioButton radioButton, Expression<Func<T>> expression)
        {
            Bind(() => radioButton.Checked, expression);
        }

        /// <summary>
        /// コンボボックスの値バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comboBox">コンボボックスオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void Bind<T>(this ComboBox comboBox, Expression<Func<T>> expression)
        {
            Bind(() => comboBox.SelectedItem, expression);
        }

        /// <summary>
        /// コンボボックスのデータソースバインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comboBox">コンボボックスオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        /// <param name="valueMember">値のメンバ名。</param>
        /// <param name="displayMenber">表示値のメンバ名。</param>
        public static void BindDataSource<T>(this ComboBox comboBox, Expression<Func<T>> expression, string valueMember, string displayMenber)
        {
            Bind(() => comboBox.DataSource, expression);
            comboBox.ValueMember = valueMember;
            comboBox.DisplayMember = displayMenber;
        }

        /// <summary>
        /// コントロールの活性バインドを行います。
        /// </summary>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void BindEnabled<T>(this Control control, Expression<Func<T>> expression)
        {
            Bind(() => control.Enabled, expression);
        }

        /// <summary>
        /// ボタンの表示バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="button">ボタンオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void BindVisible<T>(this Button button, Expression<Func<T>> expression)
        {
            Bind(() => button.Visible, expression);
        }

        /// <summary>
        /// パネルの表示バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="panel">パネルオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void BindVisible<T>(this ScrollableControl panel, Expression<Func<T>> expression)
        {
            Bind(() => panel.Visible, expression);
        }
    }

}
