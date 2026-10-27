// Property title animated with React Bits' SplitText (GSAP-based; respects
// prefers-reduced-motion internally). Rendered as the page's <h1>.
import SplitText from '../vendor/react-bits/SplitText.jsx';

export default function SplitTitle({ text = '', className = 'detail-title' }) {
  return (
    <SplitText
      tag="h1"
      text={text}
      className={className}
      splitType="words"
      effect="rise"
      delay={45}
      duration={0.9}
      textAlign="left"
      threshold={0}
      rootMargin="0px"
    />
  );
}
