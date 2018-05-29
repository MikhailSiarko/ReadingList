using Dapper;

namespace ReadingList.ReadModel.FluentSqlBuilder
{
    public class FluentSqlBuilder
    {
        private readonly SqlBuilder _sqlBuilder;
        private SqlBuilder.Template _template;
        private string _from;
        private string _where;
        private string _leftJoin;
        private const string WherePlaceholder = "/**where**/";
        private const string LeftJoinPlaceholder = "/**leftjoin**/";
        private const string TemplatePattern = "SELECT /**select**/ FROM {0}";
        public FluentSqlBuilder(string select, string from, string where = null, object param = null)
        {
            _sqlBuilder = new SqlBuilder();
            _sqlBuilder.Select(select);
            if (where != null)
            {
                _sqlBuilder.Where(where);
            }
            if (param != null)
            {
                _sqlBuilder.AddParameters(param);
            }

            _template = _sqlBuilder.AddTemplate(string.Format(TemplatePattern, from));
        }

        private FluentSqlBuilder()
        {
            _sqlBuilder = new SqlBuilder();
        }

        public FluentSqlBuilder Select(string select)
        {
            _sqlBuilder.Select(select);
            return this;
        }

        public FluentSqlBuilder From(string from)
        {
            _from = from;
            _template = _sqlBuilder.AddTemplate(string.Format(TemplatePattern, _from));
            return this;
        }

        public FluentSqlBuilder Where(string where)
        {
            _where = where;
            _sqlBuilder.Where(where);
            _template = BuildTemplate(string.Format(
                TemplatePattern + $" {(!string.IsNullOrEmpty(_leftJoin) ? LeftJoinPlaceholder : string.Empty)}" +
                $" {WherePlaceholder}", _from));
            return this;
        }

        public FluentSqlBuilder LeftJoin(string join)
        {
            _leftJoin = join;
            _sqlBuilder.LeftJoin(join);
            _template = BuildTemplate(string.Format(
                TemplatePattern + $" {LeftJoinPlaceholder}" +
                $" {(!string.IsNullOrEmpty(_where) ? WherePlaceholder : string.Empty)}", _from));
            return this;
        }

        public FluentSqlBuilder AddParameters(object param)
        {
            _sqlBuilder.AddParameters(param);
            return this;
        }

        public SqlResult Build() => new SqlResult {RawSql = _template?.RawSql, Parameters = _template?.Parameters};

        public object GetParameters() => _template?.Parameters;
        public string GetSqlString() => _template?.RawSql;

        private SqlBuilder.Template BuildTemplate(string template) => _sqlBuilder.AddTemplate(template);
        
        public static FluentSqlBuilder NewBuilder() => new FluentSqlBuilder();
    }
}