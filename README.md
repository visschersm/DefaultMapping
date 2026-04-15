# DefaultMapping

Dit project demonstreert verschillende manieren om `Blog`-entiteiten naar `BlogTitleView` te mappen in een .NET 5/EF Core + AutoMapper setup.

## Welke mappings worden gedemonstreerd?

- **Handmatig (old school):** entiteiten ophalen en daarna zelf projecteren naar een viewmodel.
- **Handmatige projectie in query:** direct `.Select(...)` gebruiken om `BlogTitleView` op te bouwen.
- **AutoMapper na ophalen:** eerst entiteiten laden en daarna `mapper.Map<BlogTitleView[]>` uitvoeren.
- **AutoMapper `ProjectTo` (expliciet):** `Get()` projecteert expliciet naar `BlogTitleView`.
- **AutoMapper `ProjectTo` (generiek):** `Get<TView>()` projecteert generiek naar elk viewmodel.
- **Select-expressie (generiek):** `Get<TView>(Expression<Func<Blog, TView>>)` met een expliciete select-expressie.

Daarnaast laat het project een **conventie-gebaseerde mappingregistratie** zien via:

- `IMappingOf<TEntity>` voor een enkelvoudige mapping (`CreateMap`)
- `IReverseMappingOf<TEntity>` voor bidirectionele mapping (`CreateMap(...).ReverseMap()`)
- `MappingProfile` dat deze mappings automatisch uit assemblies ophaalt en registreert.
