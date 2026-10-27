// GSAP motion for the public site. Everything is wrapped in gsap.matchMedia so
// animations are reverted (inline styles cleaned up) if the user switches to
// reduced motion, and nothing is hidden before JS runs.
import { gsap } from 'gsap';
import { ScrollTrigger } from 'gsap/ScrollTrigger';
import { SplitText } from 'gsap/SplitText';

gsap.registerPlugin(ScrollTrigger, SplitText);

const fontsReady = () =>
  Promise.race([document.fonts?.ready ?? Promise.resolve(), new Promise((r) => setTimeout(r, 700))]);

export function initMotion() {
  const mm = gsap.matchMedia();

  mm.add('(prefers-reduced-motion: no-preference)', () => {
    document.documentElement.classList.add('motion-ready');
    const splits = [];

    // Hero entrance -------------------------------------------------------
    const heroTitle = document.querySelector('.hero-title');
    const heroFades = gsap.utils.toArray('.hero [data-hero-fade], .page-hero [data-hero-fade]');
    if (heroFades.length) gsap.set(heroFades, { autoAlpha: 0, y: 22 });
    if (heroTitle) gsap.set(heroTitle, { autoAlpha: 0 });

    const failsafe = setTimeout(() => gsap.set([heroTitle, ...heroFades].filter(Boolean), { autoAlpha: 1, y: 0 }), 2000);

    fontsReady().then(() => {
      clearTimeout(failsafe);
      const tl = gsap.timeline({ defaults: { ease: 'expo.out' } });
      if (heroTitle) {
        const split = SplitText.create(heroTitle, { type: 'lines,words', mask: 'lines', linesClass: 'split-line' });
        splits.push(split);
        tl.set(heroTitle, { autoAlpha: 1 })
          .from(split.words, { yPercent: 115, duration: 1.05, stagger: 0.045 }, 0);
      }
      if (heroFades.length) {
        tl.to(heroFades, { autoAlpha: 1, y: 0, duration: 0.8, stagger: 0.08, ease: 'power3.out' }, heroTitle ? 0.25 : 0);
      }
    });

    // Hero image drift (scrubbed, transform only) --------------------------
    const heroImg = document.querySelector('.hero-media img');
    if (heroImg) {
      gsap.fromTo(heroImg, { scale: 1.08 }, {
        scale: 1,
        yPercent: 6,
        ease: 'none',
        scrollTrigger: { trigger: '.hero', start: 'top top', end: 'bottom top', scrub: true },
      });
    }

    // Section reveals ------------------------------------------------------
    const reveal = gsap.utils.toArray('[data-reveal]');
    if (reveal.length) {
      gsap.set(reveal, { autoAlpha: 0, y: 32 });
      ScrollTrigger.batch(reveal, {
        start: 'top 90%',
        once: true,
        onEnter: (batch) => gsap.to(batch, {
          autoAlpha: 1,
          y: 0,
          duration: 0.85,
          stagger: 0.08,
          ease: 'power3.out',
          overwrite: true,
          clearProps: 'transform,opacity,visibility',
        }),
      });
    }

    // Large footer word: gentle horizontal drift
    const word = document.querySelector('.footer-word');
    if (word) {
      gsap.fromTo(word, { xPercent: -4 }, {
        xPercent: 2,
        ease: 'none',
        scrollTrigger: { trigger: word, start: 'top bottom', end: 'bottom top', scrub: true },
      });
    }

    // Refresh after images settle (layout shift-safe)
    window.addEventListener('load', () => ScrollTrigger.refresh(), { once: true });

    return () => {
      clearTimeout(failsafe);
      splits.forEach((s) => s.revert());
      document.documentElement.classList.remove('motion-ready');
    };
  });
}
