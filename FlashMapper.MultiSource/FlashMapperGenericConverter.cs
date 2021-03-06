﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace FlashMapper.MultiSource
{
    public class FlashMapperGenericConverter<TSource1, TSource2> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TDestination>(source1, source2);
        }
    }
	
    public class FlashMapperGenericConverter<TSource1, TSource2, TSource3> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;
			private readonly TSource3 source3;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2, TSource3 source3)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
			this.source3 = source3;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TSource3, TDestination>(source1, source2, source3);
        }
    }
	
    public class FlashMapperGenericConverter<TSource1, TSource2, TSource3, TSource4> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;
			private readonly TSource3 source3;
			private readonly TSource4 source4;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2, TSource3 source3, TSource4 source4)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
			this.source3 = source3;
			this.source4 = source4;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TSource3, TSource4, TDestination>(source1, source2, source3, source4);
        }
    }
	
    public class FlashMapperGenericConverter<TSource1, TSource2, TSource3, TSource4, TSource5> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;
			private readonly TSource3 source3;
			private readonly TSource4 source4;
			private readonly TSource5 source5;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2, TSource3 source3, TSource4 source4, TSource5 source5)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
			this.source3 = source3;
			this.source4 = source4;
			this.source5 = source5;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TSource3, TSource4, TSource5, TDestination>(source1, source2, source3, source4, source5);
        }
    }
	
    public class FlashMapperGenericConverter<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;
			private readonly TSource3 source3;
			private readonly TSource4 source4;
			private readonly TSource5 source5;
			private readonly TSource6 source6;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2, TSource3 source3, TSource4 source4, TSource5 source5, TSource6 source6)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
			this.source3 = source3;
			this.source4 = source4;
			this.source5 = source5;
			this.source6 = source6;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TDestination>(source1, source2, source3, source4, source5, source6);
        }
    }
	
    public class FlashMapperGenericConverter<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;
			private readonly TSource3 source3;
			private readonly TSource4 source4;
			private readonly TSource5 source5;
			private readonly TSource6 source6;
			private readonly TSource7 source7;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2, TSource3 source3, TSource4 source4, TSource5 source5, TSource6 source6, TSource7 source7)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
			this.source3 = source3;
			this.source4 = source4;
			this.source5 = source5;
			this.source6 = source6;
			this.source7 = source7;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TDestination>(source1, source2, source3, source4, source5, source6, source7);
        }
    }
	
    public class FlashMapperGenericConverter<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;
			private readonly TSource3 source3;
			private readonly TSource4 source4;
			private readonly TSource5 source5;
			private readonly TSource6 source6;
			private readonly TSource7 source7;
			private readonly TSource8 source8;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2, TSource3 source3, TSource4 source4, TSource5 source5, TSource6 source6, TSource7 source7, TSource8 source8)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
			this.source3 = source3;
			this.source4 = source4;
			this.source5 = source5;
			this.source6 = source6;
			this.source7 = source7;
			this.source8 = source8;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TDestination>(source1, source2, source3, source4, source5, source6, source7, source8);
        }
    }
	
    public class FlashMapperGenericConverter<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;
			private readonly TSource3 source3;
			private readonly TSource4 source4;
			private readonly TSource5 source5;
			private readonly TSource6 source6;
			private readonly TSource7 source7;
			private readonly TSource8 source8;
			private readonly TSource9 source9;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2, TSource3 source3, TSource4 source4, TSource5 source5, TSource6 source6, TSource7 source7, TSource8 source8, TSource9 source9)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
			this.source3 = source3;
			this.source4 = source4;
			this.source5 = source5;
			this.source6 = source6;
			this.source7 = source7;
			this.source8 = source8;
			this.source9 = source9;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TDestination>(source1, source2, source3, source4, source5, source6, source7, source8, source9);
        }
    }
	
    public class FlashMapperGenericConverter<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;
			private readonly TSource3 source3;
			private readonly TSource4 source4;
			private readonly TSource5 source5;
			private readonly TSource6 source6;
			private readonly TSource7 source7;
			private readonly TSource8 source8;
			private readonly TSource9 source9;
			private readonly TSource10 source10;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2, TSource3 source3, TSource4 source4, TSource5 source5, TSource6 source6, TSource7 source7, TSource8 source8, TSource9 source9, TSource10 source10)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
			this.source3 = source3;
			this.source4 = source4;
			this.source5 = source5;
			this.source6 = source6;
			this.source7 = source7;
			this.source8 = source8;
			this.source9 = source9;
			this.source10 = source10;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TDestination>(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10);
        }
    }
	
    public class FlashMapperGenericConverter<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;
			private readonly TSource3 source3;
			private readonly TSource4 source4;
			private readonly TSource5 source5;
			private readonly TSource6 source6;
			private readonly TSource7 source7;
			private readonly TSource8 source8;
			private readonly TSource9 source9;
			private readonly TSource10 source10;
			private readonly TSource11 source11;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2, TSource3 source3, TSource4 source4, TSource5 source5, TSource6 source6, TSource7 source7, TSource8 source8, TSource9 source9, TSource10 source10, TSource11 source11)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
			this.source3 = source3;
			this.source4 = source4;
			this.source5 = source5;
			this.source6 = source6;
			this.source7 = source7;
			this.source8 = source8;
			this.source9 = source9;
			this.source10 = source10;
			this.source11 = source11;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TDestination>(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11);
        }
    }
	
    public class FlashMapperGenericConverter<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;
			private readonly TSource3 source3;
			private readonly TSource4 source4;
			private readonly TSource5 source5;
			private readonly TSource6 source6;
			private readonly TSource7 source7;
			private readonly TSource8 source8;
			private readonly TSource9 source9;
			private readonly TSource10 source10;
			private readonly TSource11 source11;
			private readonly TSource12 source12;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2, TSource3 source3, TSource4 source4, TSource5 source5, TSource6 source6, TSource7 source7, TSource8 source8, TSource9 source9, TSource10 source10, TSource11 source11, TSource12 source12)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
			this.source3 = source3;
			this.source4 = source4;
			this.source5 = source5;
			this.source6 = source6;
			this.source7 = source7;
			this.source8 = source8;
			this.source9 = source9;
			this.source10 = source10;
			this.source11 = source11;
			this.source12 = source12;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TDestination>(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, source12);
        }
    }
	
    public class FlashMapperGenericConverter<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;
			private readonly TSource3 source3;
			private readonly TSource4 source4;
			private readonly TSource5 source5;
			private readonly TSource6 source6;
			private readonly TSource7 source7;
			private readonly TSource8 source8;
			private readonly TSource9 source9;
			private readonly TSource10 source10;
			private readonly TSource11 source11;
			private readonly TSource12 source12;
			private readonly TSource13 source13;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2, TSource3 source3, TSource4 source4, TSource5 source5, TSource6 source6, TSource7 source7, TSource8 source8, TSource9 source9, TSource10 source10, TSource11 source11, TSource12 source12, TSource13 source13)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
			this.source3 = source3;
			this.source4 = source4;
			this.source5 = source5;
			this.source6 = source6;
			this.source7 = source7;
			this.source8 = source8;
			this.source9 = source9;
			this.source10 = source10;
			this.source11 = source11;
			this.source12 = source12;
			this.source13 = source13;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TDestination>(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, source12, source13);
        }
    }
	
    public class FlashMapperGenericConverter<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;
			private readonly TSource3 source3;
			private readonly TSource4 source4;
			private readonly TSource5 source5;
			private readonly TSource6 source6;
			private readonly TSource7 source7;
			private readonly TSource8 source8;
			private readonly TSource9 source9;
			private readonly TSource10 source10;
			private readonly TSource11 source11;
			private readonly TSource12 source12;
			private readonly TSource13 source13;
			private readonly TSource14 source14;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2, TSource3 source3, TSource4 source4, TSource5 source5, TSource6 source6, TSource7 source7, TSource8 source8, TSource9 source9, TSource10 source10, TSource11 source11, TSource12 source12, TSource13 source13, TSource14 source14)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
			this.source3 = source3;
			this.source4 = source4;
			this.source5 = source5;
			this.source6 = source6;
			this.source7 = source7;
			this.source8 = source8;
			this.source9 = source9;
			this.source10 = source10;
			this.source11 = source11;
			this.source12 = source12;
			this.source13 = source13;
			this.source14 = source14;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TDestination>(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, source12, source13, source14);
        }
    }
	
    public class FlashMapperGenericConverter<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15> : IFlashMapperGenericConverter
    {
        private readonly IMappingConfiguration mappingConfiguration;
			private readonly TSource1 source1;
			private readonly TSource2 source2;
			private readonly TSource3 source3;
			private readonly TSource4 source4;
			private readonly TSource5 source5;
			private readonly TSource6 source6;
			private readonly TSource7 source7;
			private readonly TSource8 source8;
			private readonly TSource9 source9;
			private readonly TSource10 source10;
			private readonly TSource11 source11;
			private readonly TSource12 source12;
			private readonly TSource13 source13;
			private readonly TSource14 source14;
			private readonly TSource15 source15;

        public FlashMapperGenericConverter(IMappingConfiguration mappingConfiguration, TSource1 source1, TSource2 source2, TSource3 source3, TSource4 source4, TSource5 source5, TSource6 source6, TSource7 source7, TSource8 source8, TSource9 source9, TSource10 source10, TSource11 source11, TSource12 source12, TSource13 source13, TSource14 source14, TSource15 source15)
        {
            this.mappingConfiguration = mappingConfiguration;
			this.source1 = source1;
			this.source2 = source2;
			this.source3 = source3;
			this.source4 = source4;
			this.source5 = source5;
			this.source6 = source6;
			this.source7 = source7;
			this.source8 = source8;
			this.source9 = source9;
			this.source10 = source10;
			this.source11 = source11;
			this.source12 = source12;
			this.source13 = source13;
			this.source14 = source14;
			this.source15 = source15;
        }

        public TDestination To<TDestination>()
        {
            return mappingConfiguration.Convert<TSource1, TSource2, TSource3, TSource4, TSource5, TSource6, TSource7, TSource8, TSource9, TSource10, TSource11, TSource12, TSource13, TSource14, TSource15, TDestination>(source1, source2, source3, source4, source5, source6, source7, source8, source9, source10, source11, source12, source13, source14, source15);
        }
    }
	
}