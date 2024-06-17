﻿using MyRecipes.Models;

namespace MyRecipes.Data
{
    public class AppDbInitialiser
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            { 
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                if (!context.Dishes.Any())
                {
                    context.Dishes.AddRange(new List<Dish>()
                    { new Dish()
                        {
                            DishName ="Baked Bkintz Casserole",
                        },
                        new Dish()
                        {
                            DishName ="Layer Cake (Mishpacha)",
                            Servings = 36,
                            Notes = "For best results freeze before slicing."
                        },
                       });
                    context.SaveChanges();
                }
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            CategoryName = "Dairy"
                        },
                        new Category()
                        {
                            CategoryName = "Casseroles"
                        },
                        new Category()
                        {
                            CategoryName = "Cakes"
                        },
                        });
                    context.SaveChanges();
                }
                if (!context.DishCategories.Any())
                {
                    context.DishCategories.AddRange(new List<DishCategory>()
                    {
                        new DishCategory()
                        {
                            DishId = 1,
                            CategoryId = 1,
                        },
                        new DishCategory()
                        {
                            DishId = 1,
                            CategoryId = 2,
                        },
                        new DishCategory()
                        {
                            DishId = 2,
                            CategoryId = 3,
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.Instructions.Any())
                {
                    context.Instructions.AddRange(new List<Instruction>()
                    {
                        new Instruction()
                        {
                            InstructionNumber = 1,
                            InstructionBody = "Preheat oven to 160. Butter a 9 by 13 inch tin or coat it with non stick spray.",
                            DishId = 1,
                        },
                        new Instruction()
                        {
                            InstructionNumber = 2,
                            InstructionBody = "Blend all batter ingredients until very smooth.",
                            DishId = 1,
                        },
                        new Instruction()

                        {
                            InstructionNumber = 3,
                            InstructionBody = "Take 1.5 cups of the batter and pour in tin. Bake for about 10 minu or so until set.",
                            DishId = 1,
                        },
                        new Instruction()

                        {
                            InstructionNumber = 4,
                            InstructionBody = "Mix well all filling ingredients. Spread the filling over the baked batter, smoothing the top. Give the remaining batter a brief stir to re-suspend the ingredients, then very slowly pour it over the cheese filling so the filling is completely covered.",
                            DishId = 1,
                        },
                        new Instruction()

                        {
                            InstructionNumber = 5,
                            InstructionBody = "Carefully return the casserole to the oven and bake an additional 35-40 min or until the top is puffed and set.",
                            DishId = 1,
                        },
                        new Instruction()

                        {
                            InstructionNumber = 6,
                            InstructionBody = "Garnish with sour cream, yoghurt, applesauce and/or fruit.",
                            DishId = 1,
                        },
                        new Instruction()

                        {
                            InstructionNumber = 1,
                            InstructionBody = "Combine all shortbread ingredients and mix. Refrigerate until needed.",
                            DishId = 2,
                        },
                        new Instruction()

                        {
                            InstructionNumber = 2,
                            InstructionBody = "Over very low flame or double boiler, melt chocolate, sugar, cocoa and water stirring occasionally. Remove from heat and stir in the margarine. Allow to cool.",
                            DishId = 2,
                        },
                         new Instruction()

                        {
                            InstructionNumber = 3,
                            InstructionBody = "Beat the egg whites until stiff. Slowly add in all the yolks, then the flour and baking powder. Fold in the chocolate mixture and mix by hand.",
                            DishId = 2,
                        },
                          new Instruction()

                        {
                            InstructionNumber = 4,
                            InstructionBody = "Roll out the shortbread dough to fit into a baking sheet. Top with fudge cake mixture. Bake at 350 for 50-60 minutes, until baked through.",
                            DishId = 2,
                        },
                           new Instruction()

                        {
                            InstructionNumber = 5,
                            InstructionBody = "Roll out a sheet of flaky dough to fit a cookie sheet, and pierce with a fork. Bake at 350 until golden.",
                            DishId = 2,
                        },
                            new Instruction()

                        {
                            InstructionNumber = 6,
                            InstructionBody = "Pipe the custard on top of the cooled cake. Place the baked flaky dough on top. Cut the cake to the desired size. Sprinkle with confectioner's sugar.",
                            DishId = 2,
                        },

                    });
                    context.SaveChanges();

                }
                if (!context.Recipes.Any())
                {
                    context.Recipes.AddRange(new List<Recipe>()
                    {
                        new Recipe()
                        {
                            RecipeName = "Batter",
                            DishId = 1,
                        },
                         new Recipe()
                        {
                            RecipeName = "Filling",
                            DishId = 1,
                        },
                          new Recipe()
                        {
                            RecipeName = "For Serving",
                            DishId = 1,
                        },
                           new Recipe()
                        {
                            RecipeName = "Shortbread Layer",
                            DishId = 2,
                        },
                            new Recipe()
                        {
                            RecipeName = "Fudge Cake Layer",
                            DishId = 2,
                        },
                    });
                    context.SaveChanges();

                }
                if (!context.Units.Any())
                {
                    context.Units.AddRange(new List<Unit>()
                    {
                        new Unit()
                        {
                            UnitName = "cups",
                        },
                        new Unit()
                        {
                            UnitName = "Tbsp",
                        },
                        new Unit()
                        {
                            UnitName = "tsp",
                        },
                        new Unit()
                        {
                            UnitName = "gr",
                        },
                        new Unit()
                        {
                            UnitName = "sticks",
                        },
                        new Unit()
                        {
                            UnitName = "oz",
                        },
                        new Unit()
                        {
                            UnitName = "sheets",
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.Ingredients.Any())
                {
                    context.Ingredients.AddRange(new List<Ingredient>()
                    {
                        new Ingredient()
                        {
                            IngredientName = "large egg",
                        },
                        new Ingredient()
                        {
                            IngredientName = "milk",
                        },
                        new Ingredient()
                        {
                            IngredientName = "sour cream",
                        },
                        new Ingredient()
                        {
                            IngredientName = "melted butter",
                        },
                        new Ingredient()
                        {
                            IngredientName = "vanilla extract",
                        },
                        new Ingredient()
                        {
                            IngredientName = "flour",
                        },
                        new Ingredient()
                        {
                            IngredientName = "sugar",
                        },
                        new Ingredient()
                        {
                            IngredientName = "baking powder",
                        },
                        new Ingredient()
                        {
                            IngredientName = "curd style farmer cheese",
                        },
                        new Ingredient()
                        {
                            IngredientName = "ricotta cheese",
                        },
                        new Ingredient()
                        {
                            IngredientName = "lemon juice",
                        },
                        new Ingredient()
                        {
                            IngredientName = "plain/vanilla yoghurt",
                        },
                        new Ingredient()
                        {
                            IngredientName = "applesauce",
                        },
                        new Ingredient()
                        {
                            IngredientName = "sliced fresh strawberries or other fruit",
                        },
                        new Ingredient()
                        {
                            IngredientName = "margarine",
                        },
                        new Ingredient()
                        {
                            IngredientName = "egg yolks",
                        },
                        new Ingredient()
                        {
                            IngredientName = "water",
                        },
                        new Ingredient()
                        {
                            IngredientName = "salt",
                        },
                        new Ingredient()
                        {
                            IngredientName = "chocolate",
                        },
                        new Ingredient()
                        {
                            IngredientName = "cocoa",
                        },
                        new Ingredient()
                        {
                            IngredientName = "separated egg",
                        },
                        new Ingredient()
                        {
                            IngredientName = "bakers choice custard",
                        },
                        new Ingredient()
                        {
                            IngredientName = "flaky dough",
                        },
                        new Ingredient()
                        {
                            IngredientName = "confectioner's sugar",
                        }

                    });
                    context.SaveChanges();
                }
                if (!context.RecipeIngredients.Any())
                {
                    context.RecipeIngredients.AddRange(new List<RecipeIngredient>()
                    {
                        new RecipeIngredient()
                        {
                            RecipeId = 1,
                            IngredientId = 1,
                            Quantity = 4,
                        },
                         new RecipeIngredient()
                        {
                            RecipeId = 1,
                            IngredientId = 2,
                            UnitId = 1,
                            Quantity = 1.25,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 1,
                            IngredientId = 3,
                            UnitId = 2,
                            Quantity = 2,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 1,
                            IngredientId = 4,
                            UnitId = 1,
                            Quantity = 0.25,
                        },
                         new RecipeIngredient()
                        {
                            RecipeId = 1,
                            IngredientId = 5,
                            UnitId = 3,
                            Quantity = 0.75,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 1,
                            IngredientId = 6,
                            UnitId = 1,
                            Quantity = 1.33,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 1,
                            IngredientId = 7,
                            UnitId = 2,
                            Quantity = 1.5,
                        },
                           new RecipeIngredient()
                        {
                            RecipeId = 1,
                            IngredientId = 8,
                            UnitId = 3,
                            Quantity = 1.25,
                        },
                         new RecipeIngredient()
                        {
                            RecipeId = 2,
                            IngredientId = 9,
                            UnitId = 4,
                            Quantity = 225,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 2,
                            IngredientId = 10,
                            UnitId = 4,
                            Quantity = 450,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 2,
                            IngredientId = 1,
                            Quantity = 2,
                        },
                         new RecipeIngredient()
                        {
                            RecipeId = 2,
                            IngredientId = 7,
                            UnitId = 2,
                            Quantity = 2.5,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 2,
                            IngredientId = 11,
                            UnitId = 2,
                            Quantity = 2,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 3,
                            IngredientId = 3,
                        },
                         new RecipeIngredient()
                        {
                            RecipeId = 3,
                            IngredientId = 12,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 3,
                            IngredientId = 13,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 3,
                            IngredientId = 14,
                        },
                         new RecipeIngredient()
                        {
                            RecipeId = 4,
                            IngredientId = 6,
                            UnitId = 1,
                            Quantity = 3,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 4,
                            IngredientId = 8,
                            UnitId = 3,
                            Quantity = 1,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 4,
                            IngredientId = 15,
                            UnitId = 5,
                            Quantity = 3,
                        },
                         new RecipeIngredient()
                        {
                            RecipeId = 4,
                            IngredientId = 16,
                            Quantity = 2,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 4,
                            IngredientId = 17,
                            UnitId = 1,
                            Quantity = 0.33,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 4,
                            IngredientId = 7,
                            UnitId = 2,
                            Quantity = 2,
                        },
                         new RecipeIngredient()
                        {
                            RecipeId = 4,
                            IngredientId = 18,
                            UnitId = 3,
                            Quantity = 0.5,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 5,
                            IngredientId = 19,
                            UnitId = 6,
                            Quantity = 8,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 5,
                            IngredientId = 7,
                            UnitId = 1,
                            Quantity = 2,
                        },
                         new RecipeIngredient()
                        {
                            RecipeId = 5,
                            IngredientId = 20,
                            UnitId = 2,
                            Quantity = 2,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 5,
                            IngredientId = 17,
                            UnitId = 1,
                            Quantity = 0.5,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 5,
                            IngredientId = 15,
                            UnitId = 5,
                            Quantity = 1,
                        },
                         new RecipeIngredient()
                        {
                            RecipeId = 5,
                            IngredientId = 21,
                            Quantity = 7,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 5,
                            IngredientId = 16,
                            Quantity = 5,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 5,
                            IngredientId = 6,
                            UnitId = 2,
                            Quantity = 5,
                        },
                         new RecipeIngredient()
                        {
                            RecipeId = 5,
                            IngredientId = 8,
                            UnitId = 3,
                            Quantity = 1.5,
                        },
                          new RecipeIngredient()
                        {
                            RecipeId = 5,
                            IngredientId = 22,
                            UnitId = 6,
                            Quantity = 12,
                        },
                           new RecipeIngredient()
                        {
                            RecipeId = 5,
                            IngredientId = 23,
                            UnitId = 7,
                            Quantity = 2,
                        },
                            new RecipeIngredient()
                        {
                            RecipeId = 5,
                            IngredientId = 24,
                        },


                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
