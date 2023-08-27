using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository;

public class PokemonRepository : IPokemonRepository
{
    private readonly DataContext _context;

    public PokemonRepository(DataContext context)
    {
        _context = context;
    }

    public Pokemon GetPokemon(int pokeId)
    {
        return _context.Pokemon
            .Where(p => p.Id == pokeId)
            .FirstOrDefault();
    }

    public Pokemon GetPokemon(string pokeName)
    {
        return _context.Pokemon
            .Where(p => p.Name == pokeName)
            .FirstOrDefault();
    }

    public decimal GetPokemonRating(int pokeId)
    {
        IQueryable<Review> review = _context.Reviews
            .Where(p => p.Pokemon.Id == pokeId);

        if (review.Count() <= 0)
            return 0;

        return ((decimal)review.Sum(r => r.Rating) / review.Count());
    }

    public ICollection<Pokemon> GetPokemons()
    {
        return _context.Pokemon
            .OrderBy(p => p.Id)
            .ToList();
    }

    public bool PokemonExists(int pokeId)
    {
        return _context.Pokemon
            .Any(p => p.Id == pokeId);
    }
}
