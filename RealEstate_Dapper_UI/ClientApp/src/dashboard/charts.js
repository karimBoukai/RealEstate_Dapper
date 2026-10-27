// Chart.js charts for the dashboards (code-split; only loaded when a chart exists).
// Data is unchanged from the original pages:
//  - "worldwide-sales" and "salse-revenue" use the template's static sample series
//    (they were hard-coded in dashmin's main.js and are not backed by the API),
//  - "city-ads" uses the estate agent chart data from the Chart API (City / CityCount).
import {
  Chart, BarController, LineController, DoughnutController, BarElement, LineElement, PointElement,
  ArcElement, CategoryScale, LinearScale, Legend, Title, Tooltip, Filler,
} from 'chart.js';

Chart.register(BarController, LineController, DoughnutController, BarElement, LineElement, PointElement,
  ArcElement, CategoryScale, LinearScale, Legend, Title, Tooltip, Filler);

const YEARS = ['2016', '2017', '2018', '2019', '2020', '2021', '2022'];
const SAMPLE = {
  'worldwide-sales': {
    type: 'bar',
    labels: YEARS,
    series: [
      { label: 'USA', data: [15, 30, 55, 65, 60, 80, 95] },
      { label: 'UK', data: [8, 35, 40, 60, 70, 55, 75] },
      { label: 'AU', data: [12, 25, 45, 55, 65, 70, 60] },
    ],
  },
  'salse-revenue': {
    type: 'line',
    labels: YEARS,
    series: [
      { label: 'Salse', data: [15, 30, 55, 45, 70, 65, 85] },
      { label: 'Revenue', data: [99, 135, 170, 130, 190, 180, 270] },
    ],
  },
};

const css = (name) => getComputedStyle(document.documentElement).getPropertyValue(name).trim();
const palette = () => [css('--accent'), css('--cyan'), css('--info'), css('--success'), css('--warning'), css('--danger')];
const alpha = (hex, a) => {
  const m = /^#?([\da-f]{2})([\da-f]{2})([\da-f]{2})$/i.exec(hex);
  if (!m) return hex;
  return `rgba(${parseInt(m[1], 16)}, ${parseInt(m[2], 16)}, ${parseInt(m[3], 16)}, ${a})`;
};

function baseOptions(type) {
  const ink = css('--muted');
  const grid = css('--line');
  const reduced = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
  const opts = {
    responsive: true,
    maintainAspectRatio: false,
    animation: reduced ? false : { duration: 700, easing: 'easeOutQuart' },
    plugins: {
      legend: { position: 'bottom', labels: { color: ink, usePointStyle: true, boxWidth: 8, padding: 16, font: { family: css('--font-body'), weight: 600 } } },
      tooltip: {
        backgroundColor: css('--night-2'),
        titleColor: '#fff',
        bodyColor: '#e5e7ee',
        padding: 12,
        cornerRadius: 10,
        titleFont: { family: css('--font-display'), weight: 600 },
        bodyFont: { family: css('--font-body') },
      },
    },
  };
  if (type !== 'doughnut') {
    opts.scales = {
      x: { grid: { display: false }, border: { color: grid }, ticks: { color: ink, font: { family: css('--font-body') } } },
      y: { beginAtZero: true, grid: { color: grid }, border: { display: false }, ticks: { color: ink, precision: 0, font: { family: css('--font-body') } } },
    };
  }
  return opts;
}

function readJson(attr) {
  try { return JSON.parse(attr || '[]'); } catch { return []; }
}

function build(canvas) {
  const key = canvas.dataset.chart;
  const colors = palette();
  const variant = canvas.dataset.variant || 'bar';

  if (key === 'city-ads') {
    const labels = readJson(canvas.dataset.labels);
    const values = readJson(canvas.dataset.values);
    const type = variant === 'doughnut' ? 'doughnut' : 'bar';
    const bg = labels.map((_, i) => colors[i % colors.length]);
    const options = baseOptions(type);
    options.plugins.legend.display = type === 'doughnut';
    if (type === 'doughnut') options.cutout = '64%';
    return new Chart(canvas, {
      type,
      data: {
        labels,
        datasets: [{
          label: 'Advertisements',
          data: values,
          backgroundColor: type === 'bar' ? bg.map((c) => alpha(c, 0.85)) : bg,
          borderColor: type === 'doughnut' ? css('--surface') : 'transparent',
          borderWidth: type === 'doughnut' ? 3 : 0,
          borderRadius: type === 'bar' ? 8 : 0,
          maxBarThickness: 46,
        }],
      },
      options,
    });
  }

  const sample = SAMPLE[key];
  if (!sample) return null;
  const datasets = sample.series.map((s, i) => {
    const c = colors[i % colors.length];
    return sample.type === 'line'
      ? { ...s, borderColor: c, backgroundColor: alpha(c, 0.18), pointBackgroundColor: c, tension: 0.35, fill: true, borderWidth: 2 }
      : { ...s, backgroundColor: alpha(c, 0.85), borderRadius: 6, maxBarThickness: 22 };
  });
  return new Chart(canvas, { type: sample.type, data: { labels: sample.labels, datasets }, options: baseOptions(sample.type) });
}

export function initCharts() {
  let charts = [];
  const render = () => {
    charts.forEach((c) => c?.destroy());
    charts = [...document.querySelectorAll('canvas[data-chart]')].map(build);
  };
  render();
  window.addEventListener('re:theme', render);
}
