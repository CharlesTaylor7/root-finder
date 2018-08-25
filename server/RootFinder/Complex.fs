namespace RootFinder

open System

type Complex =
  struct
    val real: double
    val imag: double

    new(real, double) = Complex(real, double)

    member z.normSquared = z.real * z.real + z.imag * z.imag;
    member z.norm = Math.Sqrt z.normSquared
    member z.conjugate = Complex(z.real, -z.imag)
    member z.inverse = z.conjugate / z.normSquared
  end

  // Scalar operations
  static member (*) (s: double, z: Complex) =
    Complex(s * z.real, s * z.imag)

  static member (/) (z: Complex, s: double) =
    Complex(z.real / s, z.imag / s)

  // Field operations
  static member (+) (z1: Complex, z2: Complex) =
    Complex(z1.real + z2.real, z1.imag + z2.imag);

  static member (-) (z1: Complex, z2: Complex) =
    Complex(z1.real - z2.real, z1.imag - z2.imag);

  static member (*) (z1: Complex, z2: Complex) =
    Complex(z1.real * z2.real - z1.imag * z2.imag, z1.real * z1.imag + z1.imag * z2.real);

  static member (/) (z1: Complex, z2: Complex) =
    z1 * z2.inverse
