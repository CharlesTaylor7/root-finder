import * as React from 'react';
import katex from 'katex';

export function Latex({ input } : { input: string} ) { 
  const ref = React.useRef();
  React.useEffect(() => {
    katex.render(input, ref.current)
  }, [input]);
  return (
    <p 
      ref={ref}
      style={{flexWrap: 'wrap'}}
    />
  );
}
