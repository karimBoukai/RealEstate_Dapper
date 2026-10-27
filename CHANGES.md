# Redesign — summary of changes

Scope: `RealEstate_Dapper_UI` views, ViewComponent views, layouts and static assets. **No changes** to the API
project, database, DTOs, controllers' logic, routes, API contracts, authentication or permissions.
C# additions: crash fixes listed under "Crash fixes (round 2)", plus presentation-only `Helpers/ViewFormat.cs`, `Helpers/PropertyCardModel.cs`; one unused
`using NuGet.Configuration;` removed from `_DefaultPopularLocationsComponentPartial.cs`.

## Design
- One design system (`ClientApp/src/styles/tokens.css`): porcelain/ink palette with champagne-brass and teal
  accents, Sora (display) + Manrope (text) + Instrument Serif (accents), layered glass surfaces, light + dark themes.
- Public site: full-bleed cinematic hero with glass search panel, property cards with price overlays, editorial
  "Who we are", mosaic locations, testimonial carousel, feature cards, tips cards, dark footer; dark page heroes
  with breadcrumbs on listing/detail pages; split-screen sign-in.
- Dashboards: dark sidebar (role-dependent menus kept, grouped), sticky glass topbar, KPI tiles, themed charts,
  responsive tables (turn into labelled cards < 760 px), consistent create/edit forms with labels and actions.
- Theme toggle keeps the original contract (`<html data-theme>` + `localStorage("theme")`), now also in dashboards.
- Empty/error states for every list and component (previously a failed API call made `foreach` over a null
  model throw).

## Libraries
| Library | Use | Notes |
|---|---|---|
| Vite 8 | Compiles `ClientApp` → `wwwroot/dist` | No dev server/routing role |
| GSAP 3.15 (ScrollTrigger, SplitText) | Hero entrance, scroll reveals, subtle parallax | `gsap.matchMedia` — reverted under reduced motion; nothing hidden without JS |
| Lenis 1.3 | Smooth wheel scrolling on public pages only | Off for reduced motion and touch; anchors (incl. skip link) routed through Lenis with focus moved to target; native scroll in dashboards, forms, dialogs |
| React 19 | Two isolated islands on the property page, loaded only there (code-split) | Razor still renders the page and a no-JS fallback |
| React Bits — SplitText | Animated property title (island) | MIT + Commons Clause; used inside the app (allowed), file kept unmodified; depends only on GSAP already in use |
| Chart.js 4 | Dashboard charts, code-split, only on pages with charts | Replaces Chart.js 3.7 + a second CDN copy of 2.9 |
| Font Awesome 6 Free (+ v4 shims) | Icons, self-hosted | Shims keep DB-stored v4 icon classes (SubFeature/BottomGrid `Icon`) working |
| Bootstrap 5.1 (existing local copy) | Dashboard dropdown behaviour only | Template CSS (w3layouts / DASHMIN) no longer loaded |

Not adopted: other React Bits components (most pull in `motion`, Three.js or Hugeicons — a second animation
stack or heavy WebGL for little value); React for search/forms (would duplicate server-rendered forms).

## Bug fixes (intent was clear; no business rule changed)
- Public navbar: stray `</li>` without `<li>` around "Login".
- Home/search/last-5 links used `/Property/PropertySingle/{id}` (served only via a slug redirect) → direct `/property/{slug}/{id}`.
- "Tips and advices": `Description.Substring(0,100)` threw for short/null descriptions → safe excerpt.
- Testimonial title was rendered into a `class` attribute → shown as text.
- Search selects: placeholders had text values, so `required` never triggered and "Select Country" / "Real Estate Tour" were posted → placeholders now `value=""`.
- `PropertySingle`: bathrooms row used `ViewBag.Bathcount` (always empty) → `BathCount`; duplicate breadcrumb spacer removed.
- Admin Ads page status buttons pointed to non-existent `/Ads/ChangeStatusTo…` → existing `/Product/ProductAdsPageStatusChangeTo…`.
- Deal-of-the-day buttons pointed to `/Product/ProductDealOfTheDaye…` (no such action) → `/DealOfTheDay/…`; its "On the site" pointed to `/Product/UpdateProduct` → property page.
- Products "On the site" linked to the all-ads list → the property page; agent "On the site" likewise.
- Sidebar "My Profile" → `/Profile/MyProfile` (was `/Charts/Index`); agent sidebar "Sign out" → `/Login/Logout` (was `/Account/Logout`), "Deals of the day" → `/DealOfTheDay/Index`; duplicate "New Messages" item removed. Topbar "My Profile"/"Log Out" now link to those routes.
- Agent "See All Ads" linked to `/Property/Index` whose view model type mismatches (crash) → `/Property/Ads`.
- DASHMIN `main.js` threw on every page without its sample charts (`$("#worldwide-sales").get(0).getContext`) → replaced by guarded modules.
- Estate-agent chart component injected a second `<html>` document and Chart.js 2.9 from a CDN → one Chart.js, same data (bar + doughnut instead of two identical bars).
- EstateAgent layout called `@RenderBody()` twice (after `</html>`) → nested layout over the shared shell.
- Full-screen loading spinner blocked the page until jQuery ran → slim progress bar.
- Unused SweetAlert2 CDN script removed from `_Layout`.
- Dead placeholder links given their obvious target: header "Post an ad" → sign-in, search icon → home search, "Discover our services" → services section, "Contact our agents" → footer contact, "Request details" → `mailto:` the agent, tip cards and dashboard "Detail" buttons → the property page.

## Crash fixes (round 2)
- **ViewComponents inheriting the page model** — the error on `/property/...`: when an API call failed, typed components did `return View();`, which hands them the *parent page's* model (e.g. `ResultProductDtos` into a `List<PropertyImageDtos>` view) → `InvalidOperationException`. All 13 typed components now always pass their own model (empty list / explicit null).
- **"View not found"**: Delete and status-toggle actions (Category, Employee, Amenity, BottomGrid, PopularLocation, Service, WhoWeAre, Product, DealOfTheDay) returned `View()` on API failure but have no view → now redirect to the list with an error notice. `Property/Ads` failure path now uses `View("Index", …)`. `ToDoList/Index` had no view → added.
- **`/Property/Index` model mismatch** → maps the same API data to the view's `GetProductsForAdsPageDtos`.
- **API offline**: `HttpRequestException`/timeouts crashed whole pages → `Services/ApiUnavailableHandler` (registered for all HttpClients) turns them into a 503 so pages show empty states; the error is logged. Category dropdowns no longer throw when Categories fails.
- **Anonymous access to agent pages** threw `NullReferenceException` in `GetUserID` → `[Authorize]` on `EstateAgent/Dashboard` and `MyAdverts` (redirects to login, like the other management controllers).
- Empty 404/5xx responses now show a styled status page (`Home/StatusCode/{code}`).
- Verified: 28 pages/actions return 200 with the API returning errors, with it offline (only the property page returns its intended 404), and with it healthy.

## Performance
- Self-hosted fonts/icons, one CSS + one JS entry per area, code-split Chart.js and React (property page only).
- Hero/login/about images re-encoded as responsive WebP + JPEG (`wwwroot/img`), `fetchpriority` on the LCP image, `loading="lazy"` / `decoding="async"` below the fold, `loading="lazy"` on map and video iframes, fixed aspect ratios to avoid layout shift.
- Removed jQuery, owl.carousel, magnific-popup, waypoints, countUp, tempusdominus, moment from page loads (jQuery kept only in `_Layout` for the SignalR test page).

## Known limitations / left unchanged (would need backend or behaviour changes)
- `/Product/DeleteProduct` and `/Product/UpdateProduct` actions don't exist (Delete/Update buttons on Products/Ads 404; the UpdateProduct view now explains this).
- `ProfileController` calls `Employees/GetEmployeeByMail` and `PUT Employees`; the API exposes `api/Employee` without a by-mail route, and `User.Identity.Name` is empty (JWT has `UserName`). The page now shows a notice when the profile can't be loaded.
- Role 5 logins redirect to `/Dashboard` (as before).
- `Product/CreateProduct` has no POST action, so the admin "Add New Ad" form can't save (the agent "Create Advert" form works).
- `_PropertyAppUserComponentPartial` always loads `AppUser?id=1` (agent is not tied to the listing).
- Placeholders kept as in the original: sample dashboard charts (labelled "Sample data"), sample topbar messages/notifications, to-do "Add" box, "250 views", "Garages: 15", "1235 satisfied customers", popular posts, static map/video embeds, newsletter form (`action="#"`), sidebar links to `/Mailbox`, `/Message`, `/Settings`, `/Charts`.
- Agent "Choose a cover photo" is a file input inside a non-multipart form, so only the file name is posted (unchanged binding).
- Destructive Delete links are plain GET links without confirmation (unchanged).
