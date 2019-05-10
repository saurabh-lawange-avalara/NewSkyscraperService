using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Avalara.Skyscraper.Data.Dapper.Base
{
 public class BaseEntityModel
    {
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

    }
    
	public partial class BaseModel : BaseEntityModel, IDisposable
	{
		#region variables
		private Hashtable _Items;
		#endregion

		#region properties
		public object this[string name]
		{
			get
			{
				if (_Items == null)
					return null;

				return _Items[name];
			}
			set
			{
				if (_Items == null)
					_Items = new Hashtable();
				_Items[name] = value;
			}
		}
		#endregion

		#region public methods
		public void Dispose()
		{
		}
		#endregion
	}
}
