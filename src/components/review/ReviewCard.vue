<template>
  <base-card :image-url="employee.photo">
    <template #header>
      <h3>{{ employee.firstName }}</h3>
    </template>
    <template #body>
      <p :class="`${$options.name}__bodyText`">{{ employee.age }}</p>
    </template>
    <template #footer>
      <button>See details</button>
    </template>
  </base-card>
</template>
