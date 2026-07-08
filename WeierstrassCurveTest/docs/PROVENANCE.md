# Provenance of benchmark datasets and experimental artefacts

This document records the provenance of the datasets and artefacts associated with the manuscript:

**“Analysing Methods of Solving the Discrete Logarithm Problem on Elliptic Curves over Finite Fields.”**

The purpose of this file is to distinguish the historical benchmark datasets used for the manuscript from auxiliary or exploratory datasets and from later development versions of the codebase.

## 1. Scope of the manuscript artefacts

The empirical results reported in the manuscript were produced using the composite-order datasets with the `com-` prefix located in:

```text
WeierstrassCurveTest/Performance/Datasets/
```

The datasets associated with the manuscript are:

```text
com-data25.csv
com-data26.csv
com-data27.csv
com-data28.csv
com-data29.csv
com-data30.csv
```

These files correspond to the six field-size configurations reported in the manuscript. They contain ECDLP instances over short-Weierstrass curves, where each instance is defined in a cyclic target subgroup of composite order and is intended for evaluation under the Pohlig–Hellman decomposition workflow.

## 2. Datasets not used for the manuscript table

The repository also contains additional datasets:

```text
data25.csv
data26.csv
data27.csv
data28.csv
data29.csv
data30.csv
data35.csv
data40.csv
data45.csv
```

These files are auxiliary or exploratory datasets from related development and testing work. They are not the datasets used to produce the main benchmark table in the manuscript unless explicitly stated in a separate experiment description.

The manuscript-level benchmark should therefore be understood as relying on the `com-data*.csv` files, not on the `data*.csv` files.

## 3. Dataset schema

The primary benchmark CSV files are stored without a header row and use the following eleven-column schema:

```text
p, A, B, curve_order, P_x, P_y, P_order, Q_x, Q_y, Q_order, expected_k
```

where:

- `p` is the prime modulus of the finite field;
- `A` and `B` are the parameters of the short-Weierstrass curve;
- `curve_order` is the order of the full elliptic-curve group;
- `P_x`, `P_y` are the affine coordinates of the base point `P`;
- `P_order` is the target-subgroup order `N = ord(P)`;
- `Q_x`, `Q_y` are the affine coordinates of the target point `Q`;
- `Q_order` is the order of the point `Q`;
- `expected_k` is the expected discrete logarithm satisfying `Q = expected_k * P`.

The manuscript interprets each benchmark instance as an ECDLP instance in the cyclic subgroup:

```text
G = <P>,    N = ord(P).
```

The composite order `N` is the order relevant to the Pohlig–Hellman workflow.

## 4. Composite-order benchmark model

The `com-data*.csv` datasets were used to evaluate solver configurations under a Pohlig–Hellman decomposition workflow.

In this model, Pohlig–Hellman is not treated as a peer solver competing directly with Shanks, Pollard’s Rho, Kangaroo, Bernstein–Lange, or Las Vegas. Instead, it acts as a decomposition wrapper:

1. factorise the target-subgroup order `N = ord(P)`;
2. reduce the original ECDLP instance to smaller subproblems;
3. solve the generated subproblems with one selected base solver;
4. reconstruct the final scalar using the Chinese Remainder Theorem.

The compared configurations in the manuscript differ by the selected base solver.

## 5. Historical benchmark provenance

The datasets and benchmark results were generated during the original experimental phase of the project. The original data-generation and stochastic solver runs did not record random seeds.

Therefore, this repository does not claim bit-for-bit replay of the historical execution traces. A future run of the generator or the probabilistic solvers may produce different individual curves, walks, collisions, or low-level timing values.

The repository is intended to support:

- inspection of the archived benchmark datasets;
- validation of the mathematical consistency of the dataset rows;
- review of the maintained reference implementation;
- future replication experiments using the released generator and codebase;
- reconstruction of the aggregate benchmark logic described in the manuscript.

## 6. Current source code versus historical benchmark code

The current `master` branch contains a maintained development version of the ECDLP benchmarking framework. It may include changes made after the original experimental phase, including bug fixes, exploratory code, parameter changes, or additional curve-model experiments.

Consequently, the current `master` branch should be treated as a maintained reference implementation and development continuation. It is not claimed to be a byte-for-byte snapshot of the exact source revision used to generate every historical measurement reported in the manuscript.

Where exact historical configuration details are unavailable, the manuscript should describe the confirmed implementation choices and avoid claiming exhaustive parameter optimisation or exact deterministic replay.

## 7. Known limitations

The following limitations should be considered when interpreting the benchmark artefacts:

1. The original stochastic generation and solver runs did not record random seeds.
2. The current development branch may contain changes made after the original benchmark run.
3. The archived `com-data*.csv` files are the primary benchmark datasets for the manuscript; auxiliary `data*.csv` files are not part of the main manuscript table.
4. The benchmark field sizes are small and are intended for controlled software evaluation rather than production-grade elliptic-curve cryptographic security assessment.
5. The results characterise Pohlig–Hellman-based configurations on composite-order target subgroups and should not be interpreted as standalone rankings of the base solvers on prime-order security-grade elliptic-curve groups.
