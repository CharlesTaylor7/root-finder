
export const Complex = (real, imaginary) => { return { real: real, imag: imaginary }};
export const polar = (norm, angle) => Complex(norm * Math.cos(angle), norm * Math.sin(angle));

export const unit = Complex(1, 0);
export const zero = Complex(0, 0);

export const negate = ({real: x, imag: y}) => Complex(-x, -y);
export const conjugate = ({real: x, imag: y}) => Complex(x, -y);

export const normSquared = ({real: x, imag: y}) => x * x + y * y;
export const norm = complex => Math.sqrt(normSquared(complex));

export const inverse = c => divideScalar(conjugate(c), normSquared(c));

export const add = ({real: x1, imag: y1}, {real: x2, imag: y2}) => Complex(x1 + x2, y1 + y2);
export const subtract = ({real: x1, imag: y1}, {real: x2, imag: y2}) => Complex(x1 - x2, y1 - y2);
export const product = ({real: x1, imag: y1}, {real: x2, imag: y2}) => Complex(x1 * x2 - y1 * y2, x1 * y2 + x2 * y1);
export const divide = (c1, c2) => product(c1, inverse(c2));

export const scale = ({real: x, imag: y}, scalar) => Complex(scalar * x, scalar * y);
export const divideScalar = ({real: x, imag: y}, divisor) => Complex(x / divisor, y / divisor);

