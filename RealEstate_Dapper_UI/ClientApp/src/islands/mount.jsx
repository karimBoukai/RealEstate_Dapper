// Mounts isolated React islands inside Razor-rendered pages.
// Each island element carries its props in a child <script type="application/json">
// written by Razor; the server-rendered markup inside it is the no-JS fallback.
import { createRoot } from 'react-dom/client';

const registry = {
  'property-gallery': () => import('./PropertyGallery.jsx'),
  'split-title': () => import('./SplitTitle.jsx'),
};

function readProps(el) {
  const script = el.querySelector(':scope > script[type="application/json"]');
  if (!script) return {};
  try { return JSON.parse(script.textContent || '{}'); } catch { return {}; }
}

export function mountIslands() {
  document.querySelectorAll('[data-island]').forEach(async (el) => {
    const load = registry[el.dataset.island];
    if (!load || el.dataset.mounted) return;
    el.dataset.mounted = 'true';
    const props = readProps(el);
    try {
      const { default: Component } = await load();
      createRoot(el).render(<Component {...props} />);
    } catch (err) {
      // keep the server-rendered fallback on failure
      delete el.dataset.mounted;
      console.warn('Island failed to mount:', el.dataset.island, err);
    }
  });
}
