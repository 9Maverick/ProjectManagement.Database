﻿using ProjectManagement.Database.Data;
using ProjectManagement.Database.Domain.Entities;
using ProjectManagement.Database.Domain.Interfaces;
using ProjectManagement.Database.Domain.Models;
using ProjectManagement.Database.Panel.ViewModels.Collections.Intefaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Interfaces;
using ProjectManagement.Database.Panel.ViewModels.Entities.Row;

namespace ProjectManagement.Database.Panel.ViewModels.Collections;

public class ProjectsCollectionViewModel : IEntityCollectionViewModel<IProject>
{
    private DatabaseContext _context;

    public List<IEntityViewModel<IProject>> Entities { get; set; }
    public IProject EntityToAdd { get; set; }

    public ProjectsCollectionViewModel(DatabaseContext context)
    {
        _context = context;

        EntityToAdd = new ProjectModel();

        LoadProjects();
    }

    public void AddEntity()
    {
        if (!IsProjectValid(EntityToAdd))
            return;

        var project = new Project(EntityToAdd);

        _context.Projects.Add(project);
        _context.SaveChanges();

        Entities.Add(new ProjectRowViewModel(project));

        EntityToAdd = new ProjectModel();
    }


    private void LoadProjects()
    {
        Entities = new List<IEntityViewModel<IProject>>();

        var projectsList = _context.Projects.ToList();

        foreach (var project in projectsList)
        {
            Entities.Add(new ProjectRowViewModel(project));
        }
    }

    private bool IsProjectValid(IProject project)
    {
        return !string.IsNullOrWhiteSpace(project.Name)
            && !string.IsNullOrWhiteSpace(project.Abbreviation);
    }
}
