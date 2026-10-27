// Theme switch — same contract as the original theme-change.js:
// <html data-theme="dark|light"> + localStorage("theme"). The initial value is
// applied by a tiny inline script in <head> to avoid a flash of the wrong theme.
const KEY = 'theme';

function read() {
  try { return localStorage.getItem(KEY); } catch { return null; }
}
function write(value) {
  try { localStorage.setItem(KEY, value); } catch { /* storage unavailable */ }
}

export function initTheme(onChange) {
  const switches = document.querySelectorAll('.theme-switch input[type="checkbox"]');
  const current = read() || document.documentElement.getAttribute('data-theme') || 'light';
  switches.forEach((input) => {
    input.checked = current === 'dark';
    input.addEventListener('change', (e) => {
      const next = e.target.checked ? 'dark' : 'light';
      document.documentElement.setAttribute('data-theme', next);
      write(next);
      switches.forEach((other) => { if (other !== e.target) other.checked = e.target.checked; });
      onChange?.(next);
    });
  });
}
