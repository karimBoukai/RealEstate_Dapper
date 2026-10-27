// Public website entry. Razor renders every page; this file only enhances it.

import { initTheme } from '../shared/theme.js';
import { initMotion } from './motion.js';
import { initSmoothScroll } from './smooth.js';

const prefersReduced = window.matchMedia('(prefers-reduced-motion: reduce)');

initTheme();
const scroller = initSmoothScroll(prefersReduced);
initHeader();
initNav(scroller);
initToTop(scroller);
initCarousels();
initCardSpotlight();
initPasswordReveal();
initMotion(prefersReduced);

if (document.querySelector('[data-island]')) {
  import('../islands/mount.jsx').then((m) => m.mountIslands());
}

// ---------------------------------------------------------------------------

function initHeader() {
  const header = document.querySelector('.site-header');
  if (!header) return;
  let ticking = false;
  const update = () => {
    header.classList.toggle('is-scrolled', window.scrollY > 40);
    ticking = false;
  };
  window.addEventListener('scroll', () => {
    if (!ticking) { ticking = true; requestAnimationFrame(update); }
  }, { passive: true });
  update();
}

function initNav(scroller) {
  const toggle = document.querySelector('.nav-toggle');
  const nav = document.getElementById(toggle?.getAttribute('aria-controls') || '');
  if (!toggle || !nav) return;
  const header = toggle.closest('header');

  const setOpen = (open, { restoreFocus = true } = {}) => {
    toggle.setAttribute('aria-expanded', String(open));
    toggle.setAttribute('aria-label', open ? 'Close menu' : 'Open menu');
    nav.classList.toggle('is-open', open);
    document.body.classList.toggle('nav-open', open);
    scroller?.lock(open);
    if (open) nav.querySelector('a, button, input')?.focus();
    else if (restoreFocus) toggle.focus();
  };

  toggle.addEventListener('click', () => setOpen(toggle.getAttribute('aria-expanded') !== 'true'));
  nav.addEventListener('click', (e) => {
    if (e.target.closest('a') && toggle.getAttribute('aria-expanded') === 'true') setOpen(false, { restoreFocus: false });
  });
  document.addEventListener('keydown', (e) => {
    if (toggle.getAttribute('aria-expanded') !== 'true') return;
    if (e.key === 'Escape') { e.preventDefault(); setOpen(false); return; }
    if (e.key === 'Tab' && header) {
      // keep focus inside the header while the full-screen menu is open
      const items = [...header.querySelectorAll('a[href], button:not([disabled]), input:not([type="hidden"])')]
        .filter((el) => el.offsetParent !== null);
      const first = items[0];
      const last = items[items.length - 1];
      if (e.shiftKey && document.activeElement === first) { e.preventDefault(); last.focus(); }
      else if (!e.shiftKey && document.activeElement === last) { e.preventDefault(); first.focus(); }
    }
  });
  window.matchMedia('(min-width: 961px)').addEventListener('change', (mq) => {
    if (mq.matches && toggle.getAttribute('aria-expanded') === 'true') setOpen(false, { restoreFocus: false });
  });
}

function initToTop(scroller) {
  const btn = document.querySelector('.to-top');
  if (!btn) return;
  const update = () => btn.classList.toggle('is-visible', window.scrollY > 700);
  window.addEventListener('scroll', update, { passive: true });
  update();
  btn.addEventListener('click', () => {
    scroller ? scroller.to(0) : window.scrollTo({ top: 0 });
    document.querySelector('.brand')?.focus({ preventScroll: true });
  });
}

function initCarousels() {
  document.querySelectorAll('[data-carousel]').forEach((root) => {
    const track = root.querySelector('.carousel-track');
    const prev = root.querySelector('[data-carousel-prev]');
    const next = root.querySelector('[data-carousel-next]');
    const status = root.querySelector('.carousel-status');
    if (!track) return;
    const slides = [...track.children];
    const controls = root.querySelector('.carousel-controls');
    if (slides.length < 2) { controls?.setAttribute('hidden', ''); return; }

    const index = () => Math.round(track.scrollLeft / Math.max(1, track.clientWidth));
    const update = () => {
      const i = index();
      if (prev) prev.disabled = i <= 0;
      if (next) next.disabled = i >= slides.length - 1;
      if (status) status.textContent = `${i + 1} / ${slides.length}`;
      slides.forEach((s, n) => s.setAttribute('aria-hidden', String(n !== i)));
    };
    const go = (dir) => track.scrollBy({ left: dir * track.clientWidth, behavior: prefersReduced.matches ? 'auto' : 'smooth' });
    prev?.addEventListener('click', () => go(-1));
    next?.addEventListener('click', () => go(1));
    track.addEventListener('keydown', (e) => {
      if (e.key === 'ArrowLeft') { e.preventDefault(); go(-1); }
      if (e.key === 'ArrowRight') { e.preventDefault(); go(1); }
    });
    let t;
    track.addEventListener('scroll', () => { clearTimeout(t); t = setTimeout(update, 60); }, { passive: true });
    window.addEventListener('resize', update);
    update();
  });
}

function initCardSpotlight() {
  if (!window.matchMedia('(hover: hover) and (pointer: fine)').matches || prefersReduced.matches) return;
  document.addEventListener('pointermove', (e) => {
    const card = e.target.closest?.('.property-card');
    if (!card) return;
    const r = card.getBoundingClientRect();
    card.style.setProperty('--mx', `${e.clientX - r.left}px`);
    card.style.setProperty('--my', `${e.clientY - r.top}px`);
  }, { passive: true });
}

function initPasswordReveal() {
  document.querySelectorAll('[data-reveal-pass]').forEach((btn) => {
    const input = document.getElementById(btn.getAttribute('aria-controls'));
    if (!input) return;
    btn.addEventListener('click', () => {
      const show = input.type === 'password';
      input.type = show ? 'text' : 'password';
      btn.setAttribute('aria-pressed', String(show));
      btn.setAttribute('aria-label', show ? 'Hide password' : 'Show password');
      btn.querySelector('i')?.classList.replace(show ? 'fa-eye' : 'fa-eye-slash', show ? 'fa-eye-slash' : 'fa-eye');
    });
  });
}
