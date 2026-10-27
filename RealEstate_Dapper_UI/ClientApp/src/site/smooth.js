// Lenis smooth scrolling — public pages only (opt-in with <body data-smooth>).
// Native scrolling is kept for reduced-motion users, touch input, dashboards,
// forms and dialogs. Same-page anchors are routed through Lenis and focus is
// moved to the target so keyboard and screen-reader users land in the right place.
import Lenis from 'lenis';
import { gsap } from 'gsap';
import { ScrollTrigger } from 'gsap/ScrollTrigger';

gsap.registerPlugin(ScrollTrigger);

const HEADER_OFFSET = -88;

function focusTarget(el) {
  if (!el) return;
  if (!el.hasAttribute('tabindex') && !/^(A|BUTTON|INPUT|SELECT|TEXTAREA)$/.test(el.tagName)) {
    el.setAttribute('tabindex', '-1');
  }
  el.focus({ preventScroll: true });
}

export function initSmoothScroll(prefersReduced) {
  const native = {
    lock(on) { document.documentElement.style.overflow = on ? 'hidden' : ''; },
    to(target) {
      const el = typeof target === 'number' ? null : target;
      if (el) el.scrollIntoView({ block: 'start' });
      else window.scrollTo({ top: target || 0 });
    },
  };

  if (!document.body.hasAttribute('data-smooth') || prefersReduced.matches) {
    window.addEventListener('re:scroll-lock', (e) => native.lock(e.detail));
    return native;
  }

  const lenis = new Lenis({
    duration: 1.05,
    easing: (t) => Math.min(1, 1.001 - Math.pow(2, -10 * t)),
    smoothWheel: true,
    syncTouch: false,
    prevent: (node) => node.closest?.('dialog, [data-lenis-prevent], .carousel-track, iframe') != null,
  });

  lenis.on('scroll', ScrollTrigger.update);
  const raf = (time) => lenis.raf(time * 1000);
  gsap.ticker.add(raf);
  gsap.ticker.lagSmoothing(0);

  const api = {
    lock(on) { on ? lenis.stop() : lenis.start(); },
    to(target) { lenis.scrollTo(target, { offset: typeof target === 'number' ? 0 : HEADER_OFFSET }); },
  };

  // Same-page anchors (incl. the skip link)
  document.addEventListener('click', (e) => {
    const link = e.target.closest?.('a[href*="#"]');
    if (!link || e.defaultPrevented || e.metaKey || e.ctrlKey || e.shiftKey || e.button !== 0) return;
    const url = new URL(link.href, location.href);
    if (url.pathname !== location.pathname || url.search !== location.search || !url.hash || url.hash === '#') return;
    const target = document.getElementById(decodeURIComponent(url.hash.slice(1)));
    if (!target) return;
    e.preventDefault();
    lenis.scrollTo(target, {
      offset: target.id === 'main' ? 0 : HEADER_OFFSET,
      onComplete: () => focusTarget(target),
    });
    history.pushState(null, '', url.hash);
  });

  window.addEventListener('re:scroll-lock', (e) => api.lock(e.detail));

  // Respect a live change of the OS setting
  prefersReduced.addEventListener('change', (mq) => {
    if (mq.matches) {
      gsap.ticker.remove(raf);
      lenis.destroy();
    }
  });

  return api;
}
