namespace Common.Entities {

    public abstract class Entity  {  // 

        public int Id { get; set; }

        //public virtual string Key { //
        //    get { return string.Format("{0} {1}", this.GetType().Name, Id); }
        //}

        public abstract string Key { get; }

        public override string ToString() { //sealed 
            return Key;
        }

    }

}