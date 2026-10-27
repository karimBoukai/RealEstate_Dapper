// Dashboard entry (admin / manager / estate agent). Native scrolling only.
// Bootstrap's own bundle (wwwroot/lib/bootstrap) still powers the dropdowns.

import { initTheme } from '../shared/theme.js';

document.getElementById('spinner')?.classList.remove('show');

initTheme((theme) => window.dispatchEvent(new CustomEvent('re:theme', { detail: theme })));
initSidebar();
markActiveLink();
labelStackTables();
initBackToTop();
renderMiniCalendar(document.getElementById('calender'));

if (document.querySelector('canvas[data-chart]')) {
  import('./charts.js').then((m) => m.initCharts());
}

// ---------------------------------------------------------------------------

function initSidebar() {
  const shell = document.querySelector('.dash-shell');
  const toggles = document.querySelectorAll('[data-sidebar-toggle]');
  if (!shell || !toggles.length) return;
  const mobile = window.matchMedia('(max-width: 1080px)');
  const KEY = 'sidebar';

  try { if (localStorage.getItem(KEY) === 'collapsed' && !mobile.matches) shell.classList.add('is-collapsed'); } catch { /* ignore */ }

  const sync = () => {
    const expanded = mobile.matches ? shell.classList.contains('is-open') : !shell.classList.contains('is-collapsed');
    toggles.forEach((t) => t.setAttribute('aria-expanded', String(expanded)));
  };
  const closeMobile = (focus = true) => {
    shell.classList.remove('is-open');
    sync();
    if (focus) toggles[0].focus();
  };

  toggles.forEach((t) => t.addEventListener('click', (e) => {
    e.preventDefault();
    if (mobile.matches) {
      shell.classList.toggle('is-open');
      if (shell.classList.contains('is-open')) shell.querySelector('.sidebar a')?.focus();
    } else {
      shell.classList.toggle('is-collapsed');
      try { localStorage.setItem(KEY, shell.classList.contains('is-collapsed') ? 'collapsed' : 'expanded'); } catch { /* ignore */ }
    }
    sync();
  }));
  shell.querySelector('.sidebar-backdrop')?.addEventListener('click', () => closeMobile(false));
  document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape' && shell.classList.contains('is-open')) closeMobile();
  });
  mobile.addEventListener('change', () => { shell.classList.remove('is-open'); sync(); });
  sync();
}

function normalize(path) {
  return decodeURIComponent(path).toLowerCase().replace(/\/index\/?$/, '').replace(/\/+$/, '') || '/';
}

function markActiveLink() {
  const here = normalize(location.pathname);
  const links = [...document.querySelectorAll('.sidebar-nav a[href^="/"]')];
  let best = null;
  links.forEach((a) => {
    const p = normalize(new URL(a.href).pathname);
    if (p === here || (p !== '/' && here.startsWith(p + '/'))) {
      if (!best || p.length > normalize(new URL(best.href).pathname).length) best = a;
    }
  });
  best?.setAttribute('aria-current', 'page');
}

function labelStackTables() {
  document.querySelectorAll('table.table-stack').forEach((table) => {
    const heads = [...table.querySelectorAll('thead th')].map((th) => th.textContent.trim());
    table.querySelectorAll('tbody tr').forEach((tr) => {
      [...tr.children].forEach((cell, i) => {
        if (!cell.hasAttribute('data-label') && heads[i]) cell.setAttribute('data-label', heads[i]);
      });
    });
  });
}

function initBackToTop() {
  const btn = document.querySelector('.back-to-top');
  if (!btn) return;
  const update = () => btn.classList.toggle('is-visible', window.scrollY > 300);
  window.addEventListener('scroll', update, { passive: true });
  update();
  btn.addEventListener('click', (e) => {
    e.preventDefault();
    window.scrollTo({ top: 0, behavior: window.matchMedia('(prefers-reduced-motion: reduce)').matches ? 'auto' : 'smooth' });
    document.querySelector('.topbar a, .topbar button')?.focus({ preventScroll: true });
  });
}

// Replaces the jQuery tempusdominus inline calendar on the dashboard (display only).
function renderMiniCalendar(host) {
  if (!host) return;
  const now = new Date();
  const year = now.getFullYear();
  const month = now.getMonth();
  const locale = document.documentElement.lang || 'en';
  const first = new Date(year, month, 1);
  const startDay = (first.getDay() + 6) % 7; // Monday-first
  const daysInMonth = new Date(year, month + 1, 0).getDate();
  const prevDays = new Date(year, month, 0).getDate();
  const weekdays = [...Array(7)].map((_, i) =>
    new Date(2024, 0, 1 + i).toLocaleDateString(locale, { weekday: 'short' }).slice(0, 2));

  let cells = '';
  let day = 1 - startDay;
  for (let w = 0; w < 6; w++) {
    cells += '<tr>';
    for (let d = 0; d < 7; d++, day++) {
      let label = day;
      let cls = '';
      if (day < 1) { label = prevDays + day; cls = 'muted'; }
      else if (day > daysInMonth) { label = day - daysInMonth; cls = 'muted'; }
      else if (day === now.getDate()) cls = 'today';
      cells += `<td class="${cls}"${cls === 'today' ? ' aria-current="date"' : ''}><span>${label}</span></td>`;
    }
    cells += '</tr>';
    if (day > daysInMonth) break;
  }
  host.innerHTML = `<table class="mini-cal">
    <caption>${first.toLocaleDateString(locale, { month: 'long', year: 'numeric' })}</caption>
    <thead><tr>${weekdays.map((w) => `<th scope="col">${w}</th>`).join('')}</tr></thead>
    <tbody>${cells}</tbody></table>`;
}
