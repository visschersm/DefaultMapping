# DefaultMapping

This project demonstrates different ways to map `Blog` entities to `BlogTitleView` in a .NET 5 / EF Core + AutoMapper setup.

## Which mapping styles are demonstrated?

- **Manual (old school):** retrieve entities first, then manually project to a view model.
- **Manual projection in query:** use `.Select(...)` directly to construct `BlogTitleView`.
- **AutoMapper after retrieval:** load entities first and then run `mapper.Map<BlogTitleView[]>`.
- **AutoMapper `ProjectTo` (explicit):** `Get()` projects explicitly to `BlogTitleView`.
- **AutoMapper `ProjectTo` (generic):** `Get<TView>()` projects generically to any view model.
- **Select expression (generic):** `Get<TView>(Expression<Func<Blog, TView>>)` with an explicit select expression.

In addition, the project shows **convention-based mapping registration** through:

- `IMappingOf<TEntity>` for one-way mapping (`CreateMap`)
- `IReverseMappingOf<TEntity>` for bidirectional mapping (`CreateMap(...).ReverseMap()`)
- `MappingProfile` that automatically discovers and registers these mappings from loaded assemblies.
