import * as React from 'react';
import { renderToString } from 'katex';

export const Latex = ({ input } : { input: string} ) => (
  <p 
    dangerouslySetInnerHTML={{__html: renderToString(input)}}
    style={{flexWrap: 'wrap'}}
  />
);
