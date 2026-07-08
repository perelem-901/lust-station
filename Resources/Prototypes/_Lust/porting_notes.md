# Заметки по портированию скреллов

## TODO / Сделать позже

- Гайд по расе в guidebook
- Локализация для всего (проверить, что ничего не пропущено)
- Phase 4: Emotes (skanger, sklaugh, skpeep, sktrill, skwarble)
- Phase 5: Advanced (cold-blooded, permeable skin)

## Реализовано в Phase 3 (диета)

- **`metabolizerType: Skrell`** — новый тип метаболизатора (`_Lust/Chemistry/metabolizer_types.yml`)
- **Органы** (`_Lust/Body/Organs/skrell.yml`):
  - `OrganSkrellHeart` / `OrganSkrellLiver` / `OrganSkrellStomach` с `metabolizerTypes: [Skrell]`
- **Body prototype** обновлён: OrganHuman* → OrganSkrell*
- **Этанол**:
  - Skrell исключены из стандартного 0.1 Poison урона
  - Свой эффект: 0.3 Poison при 10+ этанола (против 0.1 при 15+ у людей)
- **Сырое мясо** (UncookedAnimalProteins): Skrell не Animal → получают урон и рвоту (условие `inverted: [Animal, Vox, Plant]` не исключает их)
- **Шоколад** (Theobromine): Skrell не Animal → безопасно (условие `type: [Animal]` не включает их)

## Реализовано в Phase 2 (статы)

- **Temperature thresholds**: `coldDamageThreshold: 280`, `heatDamageThreshold: 420`
  (Bay12: cold_level_1 = 280, heat_level_1 = 420)
- **Damage modifier set** (`_Lust/Damage/modifier_sets.yml`):
  - `Heat: 0.9` (Bay12 burn_mod = 0.9)
  - `Shock: 1.3` (Bay12 siemens_coefficient = 1.3)

## Известные проблемы / Не портировано

- **Пятна на всё тело** (spots-right/left): В Bay12 используется система нанесения на каждую часть тела по отдельности.
  В SS14 маркинг рендерит спрайт по центру персонажа, а не на конкретной части тела.
  Спрайты пятен из Bay12 содержат мужской силуэт -> выглядят неправильно на женском теле.
  Удалено из прототипов. Спрайты сохранены в `markings.rsi` для будущей доработки.

## Не реализовано (намеренно)

- Жабры/утопление: В SS14 нет системы утопления, жабры бессмысленны.
- Успокоение водой: Нет системы эффектов от нахождения в воде.
- Плавание: В SS14 нет плавания.
- Ночное зрение: Недавно удалено у ксеновида, пользователь не захотел.

## Изменено против Bay12

- **`Poison: 0.8` (toxins_mod) — удалён**: В SS14 `HealthChange` у реагентов по дефолту `ignoreResistances: true`,
  поэтому species-модификатор не влияет на токсичный урон от химикатов. Работал бы только на внешние Projectile-источники,
  которых в игре практически нет. Возможно, заменить на другую механику позже.

## Bay12-эксклюзив (нет аналога в SS14)

- **`body_temperature = null` (хладнокровность)**: В SS14 body temperature — фиксированная константа,
  нет системы динамической температуры тела = окружающей среде.
- **`oxy_mod = 1.3`**: В SS14 нет механики урона от недостатка кислорода с множителем расы.
- **`flash_mod = 1.2`**: В SS14 нет урона типа «вспышка/ослепление» с расовым множителем.
- **Давление (warning/hazard low/high pressure)**: Пороги Barotrauma — хардкодные константы
  в `Atmospherics` на уровне сервера, не переопределяются по расам без C# изменений.
