// Property gallery island: stage + thumbnail rail + full-screen <dialog> lightbox.
// Data comes from the existing _PropertySliderComponentPartial (ImageUrl list).
import { useCallback, useEffect, useRef, useState } from 'react';
import { gsap } from 'gsap';

const reduced = () => window.matchMedia('(prefers-reduced-motion: reduce)').matches;

export default function PropertyGallery({ images = [], title = 'Property' }) {
  const [index, setIndex] = useState(0);
  const stageImg = useRef(null);
  const dialog = useRef(null);
  const count = images.length;

  const go = useCallback((dir) => setIndex((i) => (i + dir + count) % count), [count]);

  useEffect(() => {
    if (!stageImg.current || reduced()) return undefined;
    const tween = gsap.fromTo(stageImg.current, { autoAlpha: 0.35, scale: 1.02 }, { autoAlpha: 1, scale: 1, duration: 0.55, ease: 'power2.out' });
    return () => tween.kill();
  }, [index]);

  useEffect(() => {
    const d = dialog.current;
    if (!d) return undefined;
    const onClose = () => window.dispatchEvent(new CustomEvent('re:scroll-lock', { detail: false }));
    d.addEventListener('close', onClose);
    return () => d.removeEventListener('close', onClose);
  }, []);

  if (!count) {
    return (
      <div className="empty-state">
        <i className="fa-regular fa-image" aria-hidden="true" />
        <strong>No photos yet</strong>
        <p>Images for this listing haven't been uploaded.</p>
      </div>
    );
  }

  const current = images[index];
  const open = () => {
    window.dispatchEvent(new CustomEvent('re:scroll-lock', { detail: true }));
    dialog.current?.showModal();
  };
  const onKey = (e) => {
    if (e.key === 'ArrowLeft') { e.preventDefault(); go(-1); }
    if (e.key === 'ArrowRight') { e.preventDefault(); go(1); }
  };

  return (
    <div className="gallery" role="region" aria-roledescription="carousel" aria-label={`${title} photos`} onKeyDown={onKey}>
      <div className="gallery-stage">
        <img ref={stageImg} src={current} alt={`${title} — photo ${index + 1} of ${count}`} decoding="async" fetchPriority="high" />
        <button type="button" className="gallery-expand" onClick={open}>
          <span className="visually-hidden">Open photo {index + 1} full screen</span>
        </button>
        {count > 1 && (
          <>
            <button type="button" className="gallery-nav prev" onClick={() => go(-1)} aria-label="Previous photo">
              <i className="fa-solid fa-arrow-left" aria-hidden="true" />
            </button>
            <button type="button" className="gallery-nav next" onClick={() => go(1)} aria-label="Next photo">
              <i className="fa-solid fa-arrow-right" aria-hidden="true" />
            </button>
          </>
        )}
        <span className="badge badge-glass gallery-counter" aria-live="polite">{index + 1} / {count}</span>
      </div>

      {count > 1 && (
        <div className="gallery-thumbs" aria-label="Choose photo">
          {images.map((src, i) => (
            <button
              type="button"
              key={`${src}-${i}`}
              aria-current={i === index ? 'true' : undefined}
              aria-label={`Show photo ${i + 1}`}
              onClick={() => setIndex(i)}
            >
              <img src={src} alt="" loading="lazy" decoding="async" />
            </button>
          ))}
        </div>
      )}

      <dialog ref={dialog} className="lightbox" aria-label={`${title} photo viewer`} onKeyDown={(e) => { onKey(e); e.stopPropagation(); }} data-lenis-prevent>
        <div className="lightbox-inner">
          <img src={current} alt={`${title} — photo ${index + 1} of ${count}`} />
          <button type="button" className="icon-btn lightbox-close" onClick={() => dialog.current?.close()} aria-label="Close viewer" autoFocus>
            <i className="fa-solid fa-xmark" aria-hidden="true" />
          </button>
          {count > 1 && (
            <>
              <button type="button" className="gallery-nav prev" onClick={() => go(-1)} aria-label="Previous photo">
                <i className="fa-solid fa-arrow-left" aria-hidden="true" />
              </button>
              <button type="button" className="gallery-nav next" onClick={() => go(1)} aria-label="Next photo">
                <i className="fa-solid fa-arrow-right" aria-hidden="true" />
              </button>
            </>
          )}
          <span className="lightbox-caption">{index + 1} / {count}</span>
        </div>
      </dialog>
    </div>
  );
}
