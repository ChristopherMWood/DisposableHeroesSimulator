import sys, csv, re

print(str(sys.argv))

if len(sys.argv) != 2:
    print("USAGE: parsedeck.py [path to csv]")
    quit()

card_type = ""

if ("_head_" in sys.argv[1]):
    print("Detected deck type: Head")
    card_type = "HeadCard"
elif ("_arms_" in sys.argv[1]):
    print("Detected deck type: Arms")
    card_type = "ArmsCard"	
elif ("_legs_" in sys.argv[1]):
    print("Detected deck type: Legs")
    card_type = "LegsCard"
elif ("_torso_" in sys.argv[1]):
    print("Detected deck type: Torso")
    card_type = "TorsoCard"
elif ("_special_" in sys.argv[1]):
    print("Detected deck type: Special (not yet supported)")
    # card_type = "SpecialCard"
    quit()
elif ("_weapon_" in sys.argv[1]):
    print("Detected deck type: Weapon (NEEDS MANUAL UPDATE ON WEAPONTYPE)")
    card_type = "WeaponCard"
    # quit()
else:
    print("Unsupported deck type. Aborting.")
    quit()

print("Attempting to parse CSV")

deck = []

with open(sys.argv[1]) as csv_file:
    csv_reader = csv.reader(csv_file, delimiter=',')
    row_count = 0
    for row in csv_reader:
        if not(row[0] in ["", "HEAD", "ARMS", "LEGS", "TORSO"]): 
            deck.append(row)
            row_count += 1
    print(f'Processed {row_count} row(s).\n\n\n\n\n')

for ridx, row in enumerate(deck):
    col_mark = 'A'
    for val in row:
        if (card_type in ["HeadCard", "ArmsCard", "LegsCard", "TorsoCard"]) and not(str_val == agl_val == per_val == 0):
            str_val = int(re.search('STR(.*)', val).group(1) if re.search('STR(.*)', val) is not None else "0")
            agl_val = int(re.search('AGL(.*)', val).group(1) if re.search('AGL(.*)', val) is not None else "0")
            per_val = int(re.search('PER(.*)', val).group(1) if re.search('PER(.*)', val) is not None else "0")
            if card_type == "TorsoCard":
                h_val = int(re.search('HEALTH(.*)', val).group(1) if re.search('HEALTH(.*)', val) is not None else "0")
                print(f'cards.Add(new {card_type}(\"{card_type[0]}-{col_mark}-{ridx + 1:02d}\", {str_val}, {agl_val}, {per_val}, {h_val}));')
            else:
                print(f'cards.Add(new {card_type}(\"{card_type[0]}-{col_mark}-{ridx + 1:02d}\", {str_val}, {agl_val}, {per_val}));')
        if (card_type == "WeaponCard"):
            dmg_val = int(re.search('DMG(.*)', val).group(1) if re.search('DMG(.*)', val) is not None else "0")
            wpn_type = "Normal" if len(val) < 8 else "SPECIAL_NEEDS_UPDATE"
            if dmg_val != 0:
                print(f'cards.Add(new {card_type}(\"{card_type[0]}-{col_mark}-{ridx + 1:02d}\", {dmg_val}, WeaponType.{wpn_type}));')
        col_mark = chr(ord(col_mark) + 1)
