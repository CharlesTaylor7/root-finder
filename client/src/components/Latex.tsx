import * as React from 'react';
import katex from 'katex';

export const Latex = ({ input } : { input: string} ) => (
  <p 
    dangerouslySetInnerHTML={{__html: katex.renderToString(input)}}
    style={{flexWrap: 'wrap'}}
  />
);
