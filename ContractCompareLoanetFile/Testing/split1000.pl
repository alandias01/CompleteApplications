$FileName="XX_ECO5239";

open(F,$FileName);
$data=<F>;
close(F);

open(N, ">".$FileName.".txt");

$data=~ s/(.{1,1000})/$1\n/g;

print N $data;

close(N);