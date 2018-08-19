// Redo in Fable or Purescript.
// The lack of overloaded operators is extremely painful.

import * as C from './complex'

const safeAdd = (c1, c2) => {
   C.add(c1 || C.zero, c2 || C.zero);  
}

export const add = (p1, p2) => {
  const result = [];
  for (let i = 0; i < Math.max(p1.length, p2.length); i++){
    result.push(safeAdd(p1[i], p2[i]));
  }
  return result;
}

export const derivative = polynomial => {
  const d = []
  for (let i = 1; i < polynomial.length; i++)
  {
    d.push(C.scale(polynomial[i], i));
  }
  return d;
}

const enumerate = n => {
  const list;
  for (let i = 0; i < n ; i++){
    list.push(i);
  }
  return list;
}

const roots_of_unity = n => enumerate(n).map(i => 2 * Math.PI / i);

// x^n - 1
const polynomial_of_unity = (n) => {
  const polynomial = []
  polynomial[0] = -1;
  polynomial[n-1] = 1;
  return polynomial;
}

// Uses Horner's Rule
const evaluateAt = (polynomial, x) => {
  let total = zero; 
  for (let i = polynomial.length - 1; i >= 0; i--) {
    total = add(product(x, total), polynomial[i]); 
  }
  return total;
}

const scale = (polynomial, scalar) => polynomial.map(coefficient => scalar * coefficient);
const interpolate = (p1, p2, t) => add(scale(p1, 1 - t, scale(p2, t)));

const newton_solve = (polynomial, guess, delta) => {
  const p = polynomial;
  const d = derivative(polynomial);
  const newton = x => { 
    // x - P(x) / D(x)
    return C.subtract(x, C.divide(evaluateAt(p, x), evaluateAt(d, x)));
  }
  let delta_squared = delta * delta;
  let x_prev;  
  let x_current = guess;
  do {
    x_prev = x_current;
    x_current = newton(x_current);
  } while (C.normSquared(C.subtract(x2 - x1)) > delta_squared);
  
  return x_current;
}

const solve = polynomial => {
  const n = polynomial.length;
  const roots = roots_of_unity(n);
  const p_start = polynomial_of_unity(n);
  const p_final = polynomial;
  const step_count = 10;
  const t = i => (i + 1) / step_count;

  return roots.map(root => {
    return enumerate(homotopy_step_count).reduce(root, (current, i) => {
      const p = interpolate(p_start, p_final, t(i));
      return newton_solve(p, current, 0.01);
    });
  });
}
