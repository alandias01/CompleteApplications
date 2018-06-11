$FileName="cmo5239_01272016";

open(F,$FileName);
$data=<F>;
close(F);

open(N, ">".$FileName.".txt");

$data=~ s/(.{1,80})/$1\n/g;

print N $data;

close(N);