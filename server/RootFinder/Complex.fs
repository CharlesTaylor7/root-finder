namespace RootFinder

open System

type Complex =
  struct
    val real: double
    val imag: double
    new (real, imag) = { real = real; imag = imag; }
  end

  member inline z.normSquared =
   z.real * z.real + z.imag * z.imag;

  member inline z.norm =
    Math.Sqrt z.normSquared

  member inline z.conjugate =
    Complex (z.real, -z.imag)

  override z.ToString() =
    let sign =
      if Math.Sign z.imag >= 0
      then '+'
      else '-'
    String.Format ("{0} {2} {1}*i", z.real, Math.Abs z.imag, sign)

  static member inline zero = Complex(0.0, 0.0)
  static member one = Complex(1.0, 0.0)

  // Unary negation
  static member inline (~-) (z: Complex) =
    Complex(-z.real, -z.imag)

  // Scalar operations
  static member inline (*) (s: double, z: Complex) =
    Complex(s * z.real, s * z.imag)

  static member inline (*) (s: int, z: Complex) =
    double s * z

  static member inline (/) (z: Complex, s: double) =
    Complex(z.real / s, z.imag / s)

  // Field operations
  static member inline (+) (z1: Complex, z2: Complex) =
    Complex(z1.real + z2.real, z1.imag + z2.imag);

  static member inline (-) (z1: Complex, z2: Complex) =
    Complex(z1.real - z2.real, z1.imag - z2.imag);

  static member inline (*) (z1: Complex, z2: Complex) =
    Complex(z1.real * z2.real - z1.imag * z2.imag, z1.real * z1.imag + z1.imag * z2.real);

  static member inline (/) (z1: Complex, z2: Complex) =
    (z1 * z2.conjugate) / z2.normSquared

module Complex =
  // Infix constructor. Looks vaguely like '+ i'.
  let inline (+|) x  y =
    Complex(double x, double y)

  let inline complex d =
    d +| 0

  let inline polar r theta =
    r * (Math.Cos theta +| Math.Sin theta)
